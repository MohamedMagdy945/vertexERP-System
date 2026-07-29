using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Shared.Results;
using VertexERP.Infrastructure.Common.Constants;
using VertexERP.Infrastructure.Common.Extensions;

namespace VertexERP.API.Middlewares;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken ct)
    {
        var (statusCode, logLevel, errors) = MapException(exception);

        httpContext.Response.StatusCode = statusCode;

        logger.Log(logLevel, exception,
             "Exception while processing {Method} {Path}: {Message}",
            httpContext.Request.Method,
             httpContext.Request.Path,
             exception.Message);

        switch (statusCode)
        {
            case StatusCodes.Status409Conflict:

                await httpContext.Response.WriteAsJsonAsync(Result<object>.Conflict(errors.First()),
                    ct);

                break;

            case StatusCodes.Status400BadRequest:

                await httpContext.Response.WriteAsJsonAsync(Result<object>.Failure(errors.First()), ct);

                break;

            default:

                await httpContext.WriteProblemDetailsAsync(
                    CreateInternalServerProblem(httpContext), ct);

                break;
        }

        return true;
    }
    private static (int StatusCode, LogLevel LogLevel, IReadOnlyList<string> Errors) MapException(Exception exception)
    {
        return exception switch
        {
            DbUpdateException { InnerException: Microsoft.Data.SqlClient.SqlException { Number: 2601 or 2627 } } => (
                StatusCodes.Status409Conflict,
                LogLevel.Warning, ["A record with the same unique value already exists."]
            ),

            DbUpdateException => (StatusCodes.Status500InternalServerError,
                LogLevel.Error, ["A database error occurred while saving changes."]
            ),

            OperationCanceledException => (StatusCodes.Status499ClientClosedRequest,
                LogLevel.Information, ["The operation was canceled."]
            ),

            _ => (StatusCodes.Status500InternalServerError,
                LogLevel.Error, ["An unexpected error occurred. Please try again later."]
            )
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