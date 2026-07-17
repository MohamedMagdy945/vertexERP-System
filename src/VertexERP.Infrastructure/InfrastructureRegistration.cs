using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.System;
using VertexERP.Infrastructure.Constants;
using VertexERP.Infrastructure.Http.Extensions;
using VertexERP.Infrastructure.Http.Services;
using VertexERP.Infrastructure.Identity.Authentication;
using VertexERP.Infrastructure.Identity.Configuration;
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

        services.AddSingleton<IAccessTokenGenerator, AccessTokenGenerator>();
        services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IClientInfoProvider, ClientInfoProvider>();

        var jwtSettings = configuration
            .GetRequiredSection(nameof(JwtSettings))
            .Get<JwtSettings>();
        services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {

            if (jwtSettings == null)
                throw new InvalidOperationException("JWT settings are not configured properly.");

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.SecretKey)
                )
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();

                    var problem = CreateProblemDetails(
                        context.HttpContext, StatusCodes.Status401Unauthorized,
                        "Unauthorized", "Authentication is required to access this resource."
                    );

                    return context.HttpContext.WriteProblemDetailsAsync(
                        problem, context.HttpContext.RequestAborted
                    );
                },

                OnForbidden = context =>
                {
                    var problem = CreateProblemDetails(
                        context.HttpContext, StatusCodes.Status403Forbidden,
                        "Forbidden", "You do not have permission to access this resource.");

                    return context.HttpContext.WriteProblemDetailsAsync(
                        problem, context.HttpContext.RequestAborted);
                }
            };
        });

        return services;
    }
    private static ProblemDetails CreateProblemDetails(HttpContext httpContext, int statusCode, string title, string detail)
    {
        var problem = new ProblemDetails
        {
            Title = title,
            Status = statusCode,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        problem.Extensions[HttpContextItemKeys.CorrelationId] = httpContext.GetCorrelationId();

        return problem;
    }
}