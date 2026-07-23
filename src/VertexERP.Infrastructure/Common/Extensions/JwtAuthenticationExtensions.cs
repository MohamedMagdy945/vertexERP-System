using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VertexERP.Infrastructure.Common.Settings;


namespace VertexERP.Infrastructure.Common.Extensions;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetRequiredSection(nameof(TokenPairSettings))
            .Get<TokenPairSettings>()!;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey)),

                    ClockSkew = TimeSpan.Zero
                };


                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();

                        var problem = CreateProblemDetails(context.HttpContext, StatusCodes.Status401Unauthorized,
                            "Unauthorized", "Authentication is required to access this resource.");

                        return context.HttpContext.WriteProblemDetailsAsync(problem, context.HttpContext.RequestAborted);
                    },


                    OnForbidden = context =>
                    {
                        var problem = CreateProblemDetails(context.HttpContext, StatusCodes.Status403Forbidden,
                            "Forbidden", "You do not have permission to access this resource.");

                        return context.HttpContext.WriteProblemDetailsAsync(problem, context.HttpContext.RequestAborted);
                    }
                };
            });

        return services;
    }


    private static ProblemDetails CreateProblemDetails(HttpContext httpContext, int statusCode, string title, string detail)
    {
        return new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Extensions =
            {
                ["traceId"] = httpContext.TraceIdentifier,
                ["correlationId"] = httpContext.GetCorrelationId()
            }
        };
    }
}