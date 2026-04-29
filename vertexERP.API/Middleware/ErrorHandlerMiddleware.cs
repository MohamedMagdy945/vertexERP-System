using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VertexERP.Application.Common.Bases;
using VertexERP.Application.Common.Exceptions;

namespace VertexERP.API.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(
        RequestDelegate next,
        IWebHostEnvironment env,
        ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var correlationId = context.Items[CorrelationIdMiddleware.HeaderName] as string;

            if (!context.Response.HasStarted)
            {
                context.Features.Set<IExceptionHandlerFeature>(new ExceptionHandlerFeature { Error = ex });
                await HandleExceptionAsync(context, ex, correlationId);
            }
        }
    }

    private async Task HandleExceptionAsync(
     HttpContext context,
     Exception ex,
     string correlationId)
    {
        var isDev = _env.IsDevelopment();

        var (statusCode, message, errors) = ex switch
        {
            ValidationAppException validationEx => (
                HttpStatusCode.BadRequest,
                "Validation failed",
                validationEx.Errors),

            UnauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                isDev ? ex.Message : "Unauthorized access.",
                null),

            ArgumentException => (
                HttpStatusCode.BadRequest,
                isDev ? ex.Message : "Invalid argument.",
                null),

            NotImplementedException => (
                HttpStatusCode.NotImplemented,
                isDev ? ex.Message : "Not implemented.",
                null),

            DbUpdateException => (
                HttpStatusCode.InternalServerError,
                isDev ? ex.Message : "Database error occurred.",
                null),

            _ => (
                HttpStatusCode.InternalServerError,
                isDev ? ex.Message : "Unexpected error occurred.",
                null)
        };

        if (ex is ValidationAppException validationExce)
        {
            _logger.LogWarning("Validation failed for request . Errors: {Errors}", validationExce.Errors);
        }
        else
        {
            _logger.LogError(ex, "CRITICAL ERROR: Unhandled exception caught");
        }

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(
            ResponseHandler.Failure<string>(
                message,
                errors ?? new(),
                context.Response.StatusCode,
                correlationId));
    }
}