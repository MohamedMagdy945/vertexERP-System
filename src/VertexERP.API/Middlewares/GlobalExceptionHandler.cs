using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Infrastructure.Common.Constants;
using VertexERP.Infrastructure.Common.Extensions;

namespace VertexERP.API.Middlewares;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, logLevel, errors) = MapException(exception);

        switch (statusCode)
        {
            default:
                logger.LogError(exception, "Unhandled exception while processing {Method} {Path}",
                    httpContext.Request.Method, httpContext.Request.Path);

                await httpContext.WriteProblemDetailsAsync(CreateInternalServerProblem(httpContext), cancellationToken);

                break;
        }

        return true;
    }

    private static (int StatusCode, LogLevel LogLevel, IReadOnlyList<string> Errors) MapException(Exception exception)
    {
        return exception switch
        {

            _ => (StatusCodes.Status500InternalServerError, LogLevel.Error, Array.Empty<string>())
        };
    }

    private static ProblemDetails CreateInternalServerProblem(HttpContext httpContext)
    {
        var problem = new ProblemDetails
        {
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An unexpected error occurred.",
            Instance = httpContext.Request.Path
        };

        problem.Extensions[HttpContextItemKeys.CorrelationId] = httpContext.GetCorrelationId();

        return problem;
    }
}