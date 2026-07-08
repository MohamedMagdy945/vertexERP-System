using Microsoft.AspNetCore.Http;

namespace VertexERP.Shared.Results;

public sealed class Result<T>
{
    public bool IsSuccess { get; init; }
    public string Message { get; init; } = string.Empty;
    public string[] Errors { get; init; } = Array.Empty<string>();
    public T? Data { get; init; }
    public int StatusCode { get; init; }

    private Result() { }

    public static Result<T> Success(T data, string message = "Request successful", int statusCode = StatusCodes.Status200OK)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static Result<T> Failure(
     string message = "Bad request",
     string[]? errors = null,
     int statusCode = StatusCodes.Status400BadRequest)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = errors ?? Array.Empty<string>(),
            StatusCode = statusCode,
        };
    }

    public static Result<T> NotFound(
        string message = "Resource not found",
        string[]? errors = null)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = errors ?? Array.Empty<string>(),
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public static Result<T> Unauthorized(string message = "Unauthorized")
    {
        return new Result<T>
        {
            IsSuccess = false,
            Message = message,
            StatusCode = StatusCodes.Status401Unauthorized
        };
    }
}