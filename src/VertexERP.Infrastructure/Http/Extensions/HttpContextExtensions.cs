using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using VertexERP.Infrastructure.Constants;

namespace VertexERP.Infrastructure.Http.Extensions;

public static class HttpContextExtensions
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static Task WriteResponseAsync<T>
        (this HttpContext context, T responseBody, int statusCode, CancellationToken cancellationToken = default)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsJsonAsync(
            responseBody, SerializerOptions, cancellationToken
        );
    }

    public static Task WriteProblemDetailsAsync
        (this HttpContext context, ProblemDetails problem, CancellationToken cancellationToken = default)
    {
        context.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        return context.Response.WriteAsJsonAsync(
            problem,
            SerializerOptions,
            cancellationToken);
    }
    public static string GetCorrelationId(this HttpContext httpContext)
    {
        return httpContext.Items[HttpContextItemKeys.CorrelationId]!.ToString()!;
    }
}