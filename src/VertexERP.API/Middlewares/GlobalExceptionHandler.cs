using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace VertexERP.API.Middlewares;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    private const string CorrelationIdKey = "CorrelationId";

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var correlationId = httpContext.Items[CorrelationIdKey]?.ToString()
                            ?? httpContext.TraceIdentifier;

        var (statusCode, title, detail, logLevel) = MapException(exception);

        logger.Log(logLevel, exception, "Unhandled exception while processing {Method} {Path}",
             httpContext.Request.Method, httpContext.Request.Path);

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["correlationId"] = correlationId;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
    private static (int StatusCode, string Title, string Detail, LogLevel LogLevel) MapException(Exception exception)
    {
        return (StatusCodes.Status500InternalServerError, "Internal Server Error",
            "An unexpected error occurred.", LogLevel.Error);
    }
}