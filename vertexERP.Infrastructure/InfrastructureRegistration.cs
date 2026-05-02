using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using VertexERP.Application.Identity.Interfaces;
using VertexERP.Infrastructure.Common.Settings;
using VertexERP.Infrastructure.Identity.Entities;
using VertexERP.Infrastructure.Identity.Identity;
using VertexERP.Infrastructure.Persistence.DbContext;
using VertexERP.Infrastructure.Persistence.Seeder;
using VertexERP.Infrastructure.Persistence.SeederRunner;

namespace VertexERP.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            var jwtSettings = configuration
              .GetSection("JwtSettings")
              .Get<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
           .AddJwtBearer("Bearer", options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = jwtSettings.Issuer,

                   ValidateAudience = true,
                   ValidAudience = jwtSettings.Audience,

                   ValidateLifetime = true,

                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(jwtSettings.AccessTokenSecret)),

                   ClockSkew = TimeSpan.Zero
               };

               options.Events = new JwtBearerEvents
               {
                   OnAuthenticationFailed = context =>
                   {
                       var logger = context.HttpContext
                           .RequestServices
                           .GetRequiredService<ILoggerFactory>()
                           .CreateLogger("JWT");

                       logger.LogWarning(
                           "JWT Authentication failed: {Message} | Path: {Path}",
                           context.Exception.Message,
                           context.Request.Path
                       );

                       return Task.CompletedTask;
                   },

                   OnChallenge = context =>
                   {
                       var logger = context.HttpContext
                           .RequestServices
                           .GetRequiredService<ILoggerFactory>()
                           .CreateLogger("JWT");

                       logger.LogWarning(
                           "Unauthorized request to {Path} | IP: {IP}",
                           context.Request.Path,
                           context.HttpContext.Connection.RemoteIpAddress
                       );

                       return Task.CompletedTask;
                   },

                   OnTokenValidated = context =>
                   {
                       var logger = context.HttpContext
                           .RequestServices
                           .GetRequiredService<ILoggerFactory>()
                           .CreateLogger("JWT");

                       var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                       logger.LogInformation(
                           "Token validated successfully for UserId: {UserId}",
                           userId
                       );

                       return Task.CompletedTask;
                   }

               };
           });


            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<PermissionService>();
            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<RefreshTokenService>();


            services.AddScoped<IDataSeeder, RoleSeeder>();
            services.AddScoped<IDataSeeder, PermissionSeeder>();
            services.AddScoped<IDataSeeder, UserSeeder>();
            services.AddScoped<DataSeederRunner>();

            return services;
        }
    }
}
