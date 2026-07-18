using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Infrastructure.Constants;
using VertexERP.Infrastructure.Http.Extensions;
using VertexERP.Shared.Exceptions;
using VertexERP.Shared.Results;

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
            case StatusCodes.Status400BadRequest:

                logger.LogWarning("Validation failed for request {Path}. Errors: {@Errors}",
                                  httpContext.Request.Path, errors);

                var correlationId = httpContext.GetCorrelationId();
                httpContext.Response.Headers["X-Correlation-Id"] = correlationId;

                var validationResult = Result<object>.ValidationFailed(errors);

                await httpContext.WriteResponseAsync(validationResult, statusCode, cancellationToken);

                break;

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
            AppValidationException validationException => (StatusCodes.Status400BadRequest, LogLevel.Warning, validationException.Errors),

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