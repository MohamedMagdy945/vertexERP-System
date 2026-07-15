namespace VertexERP.API.Middlewares;

using System.Diagnostics;

public sealed class RequestResponseLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestResponseLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        logger.LogInformation("Incoming HTTP Request: {Method} {Path}", context.Request.Method,
              context.Request.Path);


        await next(context);

        stopwatch.Stop();

        logger.LogInformation("Outgoing HTTP Response: {StatusCode} for {Method} {Path} completed in {ElapsedMilliseconds:F2} ms",
            context.Response.StatusCode, context.Request.Method, context.Request.Path, stopwatch.Elapsed.TotalMilliseconds);
    }
}