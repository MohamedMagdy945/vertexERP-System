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
                result.Errors),
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
            ValidationException validationException => new ExceptionResult(
                HttpStatusCode.BadRequest,
                "Validation failed.",
                new[]
                {
                    validationException.ValidationResult?.ErrorMessage
                    ?? validationException.Message
                }),

            UnauthorizedAccessException => new ExceptionResult(
                HttpStatusCode.Unauthorized,
                isDevelopment
                    ? exception.Message
                    : "Unauthorized access.",
                EmptyErrors),

            KeyNotFoundException => new ExceptionResult(
                HttpStatusCode.NotFound,
                isDevelopment
                    ? exception.Message
                    : "Resource not found.",
                EmptyErrors),

            ArgumentException => new ExceptionResult(
                HttpStatusCode.BadRequest,
                isDevelopment
                    ? exception.Message
                    : "Invalid request.",
                EmptyErrors),

            InvalidOperationException => new ExceptionResult(
                HttpStatusCode.BadRequest,
                isDevelopment
                    ? exception.Message
                    : "Invalid operation.",
                EmptyErrors),

            //DbUpdateException => new ExceptionResult(
            //    HttpStatusCode.InternalServerError,
            //    isDevelopment
            //        ? exception.Message
            //        : "A database error occurred.",
            //    EmptyErrors),

            TimeoutException => new ExceptionResult(
                HttpStatusCode.RequestTimeout,
                isDevelopment
                    ? exception.Message
                    : "The request timed out.",
                EmptyErrors),

            OperationCanceledException => new ExceptionResult(
                HttpStatusCode.RequestTimeout,
                "The request was cancelled.",
                EmptyErrors),

            NotImplementedException => new ExceptionResult(
                HttpStatusCode.NotImplemented,
                isDevelopment
                    ? exception.Message
                    : "This feature is not implemented.",
                EmptyErrors),

            _ => new ExceptionResult(
                HttpStatusCode.InternalServerError,
                isDevelopment
                    ? exception.Message
                    : "An unexpected error occurred.",
                EmptyErrors)
        };
    }

    private sealed record ExceptionResult(
        HttpStatusCode StatusCode,
        string Message,
        string[] Errors);
}