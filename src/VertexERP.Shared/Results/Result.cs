using Microsoft.AspNetCore.Http;

namespace VertexERP.Shared.Results;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public string[] Errors { get; private set; } = [];
    public T? Data { get; private set; }
    public int StatusCode { get; private set; }

    private Result() { }

    public static Result<T> Create() => new();

    public Result<T> Success(T data, string message = "Request successful",
        int statusCode = StatusCodes.Status200OK)
    {
        IsSuccess = true;
        Data = data;
        Message = message;
        Errors = [];
        StatusCode = statusCode;

        return this;
    }

    public Result<T> Created(
        T data, string message = "Resource created successfully")
    {
        return Success(data, message, StatusCodes.Status201Created);
    }
    public Result<T> Updated(
       T data, string message = "Resource updated successfully")
    {
        return Success(data, message, StatusCodes.Status200OK);
    }


    public Result<T> Deleted(string message = "Resource deleted successfully")
    {
        IsSuccess = true;
        Data = default;
        Message = message;
        Errors = [];
        StatusCode = StatusCodes.Status200OK;

        return this;
    }

    public Result<T> Failure(string message = "Bad request", string[]? errors = null,
        int statusCode = StatusCodes.Status400BadRequest)
    {
        IsSuccess = false;
        Data = default;
        Message = message;
        Errors = errors ?? [];
        StatusCode = statusCode;

        return this;
    }

    public Result<T> NotFound(string message = "Resource not found",
        string[]? errors = null)
    {
        IsSuccess = false;
        Data = default;
        Message = message;
        Errors = errors ?? [];
        StatusCode = StatusCodes.Status404NotFound;

        return this;
    }

    public Result<T> Unauthorized(string message = "Unauthorized")
    {
        IsSuccess = false;
        Data = default;
        Message = message;
        Errors = [];
        StatusCode = StatusCodes.Status401Unauthorized;

        return this;
    }

    public Result<T> Forbidden(string message = "Forbidden")
    {
        IsSuccess = false;
        Data = default;
        Message = message;
        Errors = [];
        StatusCode = StatusCodes.Status403Forbidden;

        return this;
    }

    public Result<T> Conflict(string message = "Conflict", string[]? errors = null)
    {
        IsSuccess = false;
        Data = default;
        Message = message;
        Errors = errors ?? [];
        StatusCode = StatusCodes.Status409Conflict;

        return this;
    }

    public Result<T> NoContent()
    {
        IsSuccess = true;
        Data = default;
        Message = string.Empty;
        Errors = [];
        StatusCode = StatusCodes.Status204NoContent;

        return this;
    }
}