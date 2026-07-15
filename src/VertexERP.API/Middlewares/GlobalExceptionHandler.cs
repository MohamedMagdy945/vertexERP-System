using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace VertexERP.API.Middlewares;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var correlationId = httpContext.Items["CorrelationId"]?.ToString()
                            ?? httpContext.TraceIdentifier;

        var (statusCode, title, detail, logLevel) = MapException(exception);

        logger.Log(logLevel, exception,
            "Unhandled exception while processing {Method} {Path}",
            httpContext.Request.Method,
            httpContext.Request.Path
            );

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

    private static (int StatusCode, string Title, string Detail, LogLevel LogLevel)
        MapException(Exception exception)
    {
        return exception switch
        {
            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                "Unauthorized",
                "You are not authorized to perform this action.",
                LogLevel.Warning),

            KeyNotFoundException => (
                StatusCodes.Status404NotFound,
                "Resource Not Found",
                "The requested resource was not found.",
                LogLevel.Information),


            InvalidOperationException => (StatusCodes.Status409Conflict, "Conflict",
                "The requested operation cannot be completed.",
                LogLevel.Warning),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred.",
                LogLevel.Error)
        };
    }
}