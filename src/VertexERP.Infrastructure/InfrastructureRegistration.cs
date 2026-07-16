using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.System;
using VertexERP.Infrastructure.Identity.Authentication;
using VertexERP.Infrastructure.Identity.Http;
using VertexERP.Infrastructure.Identity.Settings;
using VertexERP.Infrastructure.Persistence;

namespace VertexERP.Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.Configure<JwtSettings>(
            configuration.GetRequiredSection(nameof(JwtSettings)));

        services.AddSingleton<IAccessTokenGenerator, JwtAccessTokenGenerator>();
        services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
        services.AddSingleton<IClientInfoProvider, ClientInfoProvider>();
        var jwtSettings = configuration
            .GetRequiredSection(nameof(JwtSettings))
            .Get<JwtSettings>();
        services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {


            //if (jwtSettings == null)
            //    throw new InvalidOperationException("JWT settings are not configured properly.");

            //options.TokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuer = true,
            //    ValidateAudience = true,
            //    ValidateLifetime = true,
            //    ValidateIssuerSigningKey = true,
            //    ValidIssuer = jwtSettings.Issuer,
            //    ValidAudience = jwtSettings.Audience,
            //    IssuerSigningKey = new SymmetricSecurityKey(
            //        Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            //};

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();

                    await ProblemDetailsWriter.WriteAsync(
                        context.HttpContext,
                        StatusCodes.Status401Unauthorized,
                        "Unauthorized",
                        "Authentication token is missing or invalid.",
                        context.HttpContext.RequestAborted
                    );
                },

                OnForbidden = context => ProblemDetailsWriter.WriteAsync(
                    context.HttpContext,
                    StatusCodes.Status403Forbidden,
                    "Forbidden",
                    "You do not have permission to access this resource.",
                    context.HttpContext.RequestAborted
                )
            };
        });

        return services;
    }
}