using Microsoft.AspNetCore.Diagnostics;
using VertexERP.Infrastructure.Identity.Http;

namespace VertexERP.API.Middlewares;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
     CancellationToken cancellationToken)
    {
        var (statusCode, title, detail, logLevel) = MapException(exception);

        logger.Log(logLevel, exception, "Unhandled exception while processing {Method} {Path}",
             httpContext.Request.Method, httpContext.Request.Path);

        await ProblemDetailsWriter.WriteAsync(httpContext, statusCode, title, detail
            , cancellationToken);

        return true;
    }
    private static (int StatusCode, string Title, string Detail, LogLevel LogLevel) MapException(Exception exception)
    {
        return (StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred.", LogLevel.Error);
    }
}