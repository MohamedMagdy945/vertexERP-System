using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Infrastructure.Common.Constants;

namespace VertexERP.Infrastructure.Identity.Http;

public static class ProblemDetailsWriter
{
    public static Task WriteAsync(
      HttpContext httpContext, int statusCode, string title, string detail,
      CancellationToken cancellationToken = default)
    {
        var correlationId =
            httpContext.Items[HttpContextItemKeys.CorrelationId]?.ToString();

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        problem.Extensions[HttpContextItemKeys.CorrelationId] = correlationId;

        return httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
    }
}

