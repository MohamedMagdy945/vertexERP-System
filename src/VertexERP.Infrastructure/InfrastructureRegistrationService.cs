using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using VertexERP.Application.Abstractions.Authentication;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Infrastructure.Authentication;
using VertexERP.Infrastructure.Persistence;

namespace VertexERP.Infrastructure;

public static class InfrastructureRegistrationService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.LogTo(Console.WriteLine, LogLevel.Information);
        });

        services.Configure<TokenPairSettings>(
             configuration.GetSection("TokenPairSettings"));


        services.AddSingleton<ITokenGenerator, TokenPairGenerator>();

        services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

        services.AddSingleton<IClientInfoProvider, ClientInfoProvider>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        })
       .AddJwtBearer("Bearer", options =>
       {

           var tokenPairSettings = configuration
           .GetSection("TokenPairSettings")
           .Get<TokenPairSettings>();

           if (tokenPairSettings == null)
               throw new InvalidOperationException("JWT settings are not configured properly.");

           options.RequireHttpsMetadata = false;
           options.SaveToken = true;

           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidIssuer = tokenPairSettings.Issuer,

               ValidateAudience = true,
               ValidAudience = tokenPairSettings.Audience,

               ValidateLifetime = true,

               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(tokenPairSettings.AccessTokenSecret)),

               ClockSkew = TimeSpan.Zero
           };

           options.Events = new JwtBearerEvents
           {
               OnAuthenticationFailed = context =>
               {
                   Console.WriteLine(context.Exception);

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

        return services;
    }
}

