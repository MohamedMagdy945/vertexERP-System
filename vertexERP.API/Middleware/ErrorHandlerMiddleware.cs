using System.Net;
using VertexERP.Application.Common.Bases;

namespace VertexERP.API.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public ErrorHandlerMiddleware(RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var correlationId = context.Items["X-Correlation-Id"]?.ToString();


            if (!context.Response.HasStarted)
            {
                await HandleExceptionAsync(context, ex, correlationId);
            }
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, string correlationId)
    {
        context.Response.ContentType = "application/json";

        var statusCode = ex switch
        {
            NotImplementedException => HttpStatusCode.NotImplemented,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            ArgumentException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        string message = _env.IsDevelopment()
            ? ex.Message
            : "An unexpected error occurred. Please use the Correlation ID to report this issue.";

        var response = ResponseHandler.Failure<string>(
            message: message,
            statusCode: (int)statusCode,
            correlationId: correlationId
        );

        await context.Response.WriteAsJsonAsync(response);
    }
}