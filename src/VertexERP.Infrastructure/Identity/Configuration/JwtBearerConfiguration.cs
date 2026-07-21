using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VertexERP.Infrastructure.Http.Extensions;
using VertexERP.Infrastructure.Identity.Settings;

namespace VertexERP.Infrastructure.Identity.Configuration;

internal sealed class JwtBearerConfiguration(IOptions<TokenPairSettings> tokenOptions) : IConfigureNamedOptions<JwtBearerOptions>
{
    public void Configure(JwtBearerOptions options)
    {
        var settings = tokenOptions.Value;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settings.Issuer,
            ValidAudience = settings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(settings.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();

                var problem = CreateProblemDetails(
                    context.HttpContext,
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    "Authentication is required to access this resource.");

                return context.HttpContext.WriteProblemDetailsAsync(
                    problem,
                    context.HttpContext.RequestAborted);
            },

            OnForbidden = context =>
            {
                var problem = CreateProblemDetails(
                    context.HttpContext,
                    StatusCodes.Status403Forbidden,
                    "Forbidden",
                    "You do not have permission to access this resource.");

                return context.HttpContext.WriteProblemDetailsAsync(
                    problem,
                    context.HttpContext.RequestAborted);
            }
        };
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }

    private static ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int statusCode,
        string title,
        string detail)
    {
        var correlationId =
            httpContext.Items["CorrelationId"] as string;

        return new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Extensions =
            {
                ["traceId"] = httpContext.TraceIdentifier,
                ["correlationId"] = correlationId
            }
        };
    }
}