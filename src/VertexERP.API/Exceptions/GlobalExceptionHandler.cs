using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Net;
using VertexERP.Shared.Results;

namespace VertexERP.API.Exceptions;

public sealed class GlobalExceptionHandler(
    IHostEnvironment environment,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private static readonly string[] EmptyErrors = [];

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {


        var result = MapException(exception, environment.IsDevelopment());

        LogException(exception, result.Errors);

        context.Response.StatusCode = (int)result.StatusCode;

        await context.Response.WriteAsJsonAsync(
            Result<string>.Failure(
               result.Message, result.Errors),
            cancellationToken);

        return true;
    }

    private void LogException(
        Exception exception,
        string[] errors)
    {
        if (exception is ValidationException)
        {
            logger.LogWarning(
                "Validation failed. Errors: {@Errors}",
                errors);

            return;
        }

        logger.LogError(
            exception,
            "Unhandled exception occurred.");
    }



    private static ExceptionResult MapException(
        Exception exception,
        bool isDevelopment)
    {
        return exception switch
        {
            FluentValidation.ValidationException ex => new ExceptionResult(
                HttpStatusCode.BadRequest,
                "Validation failed.",
                ex.Errors
                    .Select(x => x.ErrorMessage)
                    .Distinct()
                    .ToArray()),

            UnauthorizedAccessException ex => new ExceptionResult(
                HttpStatusCode.Unauthorized,
                isDevelopment ? ex.Message : "Unauthorized access.",
                EmptyErrors),

            KeyNotFoundException ex => new ExceptionResult(
                HttpStatusCode.NotFound,
                isDevelopment ? ex.Message : "Resource not found.",
                EmptyErrors),

            ArgumentException ex => new ExceptionResult(
                HttpStatusCode.BadRequest,
                isDevelopment ? ex.Message : "Invalid request.",
                EmptyErrors),

            InvalidOperationException ex => new ExceptionResult(
                HttpStatusCode.BadRequest,
                isDevelopment ? ex.Message : "Invalid operation.",
                EmptyErrors),

            TimeoutException ex => new ExceptionResult(
                HttpStatusCode.RequestTimeout,
                isDevelopment ? ex.Message : "The request timed out.",
                EmptyErrors),

            OperationCanceledException => new ExceptionResult(
                HttpStatusCode.RequestTimeout,
                "The request was cancelled.",
                EmptyErrors),

            NotImplementedException ex => new ExceptionResult(
                HttpStatusCode.NotImplemented,
                isDevelopment ? ex.Message : "This feature is not implemented.",
                EmptyErrors),

            _ => new ExceptionResult(
                HttpStatusCode.InternalServerError,
                isDevelopment ? exception.Message : "An unexpected error occurred.",
                EmptyErrors)
        };
    }

    private sealed record ExceptionResult(
        HttpStatusCode StatusCode,
        string Message,
        string[] Errors);
}