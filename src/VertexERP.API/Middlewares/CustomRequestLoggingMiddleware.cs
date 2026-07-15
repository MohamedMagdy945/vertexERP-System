using System.Diagnostics;

namespace VertexERP.API.middleware;

public class CustomRequestLoggingMiddleware(RequestDelegate next, ILogger<CustomRequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await next(context);
        }
        finally
        {
            stopwatch.Stop();

            var correlationId = context.Items["CorrelationId"]?.ToString()
                                ?? context.Request.Headers["X-Correlation-ID"].ToString()
                                ?? Activity.Current?.Id
                                ?? context.TraceIdentifier;

            var elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
            var statusCode = context.Response.StatusCode;

            var logLevel = statusCode >= 500
      ? LogLevel.Warning
      : LogLevel.Information;

            logger.Log(
                logLevel,
                "HTTP {Method} {Path} responded {StatusCode} in {Elapsed:0.0000} ms",
                context.Request.Method,
                context.Request.Path,
                statusCode,
                elapsedMs);
        }
    }
}