using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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


            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));


            var jwtSettings = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<JwtSettings>>()
            .Value;

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
                       Console.WriteLine("Token failed: " + context.Exception.Message);
                       return Task.CompletedTask;
                   },
                   OnTokenValidated = context =>
                   {
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
