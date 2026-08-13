using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Results;
using VertexERP.Infrastructure.Common.Constants;
using VertexERP.Infrastructure.Common.Extensions;

namespace VertexERP.API.Middlewares;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken ct)
    {
            var (statusCode, logLevel, errors) = MapException(exception);

        httpContext.Response.StatusCode = statusCode;

        logger.Log(logLevel, exception,
             "Exception while processing {Method} {Path}: {Message}",
             httpContext.Request.Method,
             httpContext.Request.Path,
             exception.Message);

        var errorMessage = errors.FirstOrDefault() ?? "An unexpected error occurred.";

        switch (statusCode)
        {
            case StatusCodes.Status409Conflict:
                await httpContext.Response.WriteAsJsonAsync(
                    Result<object>.Conflict(errorMessage),
                    ct);
                break;

            case StatusCodes.Status404NotFound:
                await httpContext.Response.WriteAsJsonAsync(
                    Result<object>.NotFound(errorMessage),
                    ct);
                break;

            case StatusCodes.Status400BadRequest:
                await httpContext.Response.WriteAsJsonAsync(
                    Result<object>.Failure(errorMessage),
                    ct);
                break;

            default:
                await httpContext.WriteProblemDetailsAsync(
                    CreateInternalServerProblem(httpContext),
                    ct);
                break;
        }

        return true;
    }

    private static (int StatusCode, LogLevel LogLevel, IReadOnlyList<string> Errors)
    MapException(Exception exception)
    {
        if (exception is DbUpdateException dbEx)
        {
            if (dbEx.IsUniqueConstraintViolation())
            {
                return (
                    StatusCodes.Status409Conflict,
                    LogLevel.Warning,
                    ["A record with the same unique identifier already exists."]
                );
            }

            if (dbEx.IsForeignKeyConstraintViolation())
            {
                return (
                    StatusCodes.Status404NotFound,
                    LogLevel.Warning,
                    ["The referenced entity was not found."]
                );
            }
        }

        return exception switch
        {
            BadHttpRequestException => (
                StatusCodes.Status400BadRequest,
                LogLevel.Warning,
                ["The request is invalid or required request data is missing."]
            ),

            DbUpdateException { InnerException: Microsoft.Data.SqlClient.SqlException { Number: 2601 or 2627 } } => (
                StatusCodes.Status409Conflict,
                LogLevel.Warning,
                ["A record with the same unique identifier already exists."]
            ),

            DbUpdateException { InnerException: Microsoft.Data.SqlClient.SqlException { Number: 547 } } => (
                StatusCodes.Status404NotFound,
                LogLevel.Warning,
                ["The referenced entity was not found."]
            ),

            DbUpdateException => (
                StatusCodes.Status500InternalServerError,
                LogLevel.Error,
                ["A database error occurred while processing the request."]
            ),

            OperationCanceledException => (
                StatusCodes.Status499ClientClosedRequest,
                LogLevel.Information,
                ["The request was canceled."]
            ),

            _ => (
                StatusCodes.Status500InternalServerError,
                LogLevel.Error,
                ["An unexpected error occurred. Please try again later."]
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