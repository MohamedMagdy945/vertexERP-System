using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Infrastructure.Constants;
using VertexERP.Infrastructure.Http.Extensions;
using VertexERP.Shared.Exceptions;
using VertexERP.Shared.Results;

namespace VertexERP.API.Middlewares;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception, CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case AppValidationException { Errors: var errors }:

                var validationResult = Result<object>.ValidationFailed(errors);

                await httpContext.WriteResponseAsync(
                    validationResult,
                    StatusCodes.Status400BadRequest,
                    cancellationToken);

                break;

            default:

                logger.LogError(exception, "Unhandled exception while processing {Method} {Path}",
                    httpContext.Request.Method, httpContext.Request.Path);

                var problem = CreateInternalServerError(httpContext);

                await httpContext.WriteProblemDetailsAsync(problem, cancellationToken);

                break;
        }

        return true;
    }
    private static ProblemDetails CreateInternalServerError(HttpContext context)
    {
        var problem = new ProblemDetails
        {
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An unexpected error occurred.",
            Instance = context.Request.Path
        };

        problem.Extensions[HttpContextItemKeys.CorrelationId] = context.GetCorrelationId();

        return problem;
    }
}