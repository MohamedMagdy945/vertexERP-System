using System.Text.Json.Serialization;

namespace VertexERP.Shared.Results;

public class Result<T>
{
    [JsonIgnore]
    public ResultStatus Status { get; protected init; }

    public bool IsSuccess => Status is ResultStatus.Success or ResultStatus.Created or ResultStatus.NoContent;
    public string Message { get; protected init; } = string.Empty;
    public IReadOnlyList<string>? Errors { get; protected init; }
    public T? Data { get; protected init; }

    protected Result()
    {
    }

    public static Result<T> Success(T data, string message = "Request completed successfully.")
    {
        return new()
        {
            Status = ResultStatus.Success,
            Message = message,
            Data = data
        };
    }

    public static Result<T> Failure(string message = "The request could not be processed.",
        params string[] errors)
    {
        return new()
        {
            Status = ResultStatus.Failure,
            Message = message,
            Errors = errors
        };
    }

    public static Result<T> ValidationFailed(IReadOnlyList<string> errors)
    {
        return new()
        {
            Status = ResultStatus.ValidationFailed,
            Message = "The request contains validation errors.",
            Errors = errors
        };
    }
    public static Result<T> NotFound(string message = "The requested resource was not found.")
    {
        return new()
        {
            Status = ResultStatus.NotFound,
            Message = message
        };
    }

    public static Result<T> Conflict(string message = "The request conflicts with the current state of the resource.")
    {
        return new()
        {
            Status = ResultStatus.Conflict,
            Message = message
        };
    }

    public static Result<T> Unauthorized(string message = "Authentication is required.")
    {
        return new()
        {
            Status = ResultStatus.Unauthorized,
            Message = message
        };
    }

    public static Result<T> Forbidden(string message = "You do not have permission to perform this action.")
    {
        return new()
        {
            Status = ResultStatus.Forbidden,
            Message = message
        };
    }

    public static Result<T> NoContent(string message = "Request completed successfully.")
    {
        return new()
        {
            Status = ResultStatus.NoContent,
            Message = message
        };
    }
    public static Result<T> Created(T data, string message = "Resource created successfully.")
    {
        return new()
        {
            Status = ResultStatus.Created,
            Message = message,
            Data = data
        };
    }
    public static Result<T> BadRequest(string message = "The request is invalid.", params string[] errors)
    {
        return new()
        {
            Status = ResultStatus.BadRequest,
            Message = message,
            Errors = errors
        };
    }

    public static Result<T> Unprocessable(string message = "The request could not be processed due to semantic errors.", params string[] errors)
    {
        return new()
        {
            Status = ResultStatus.Unprocessable,
            Message = message,
            Errors = errors
        };
    }

    public static Result<T> TooManyRequests(string message = "Rate limit exceeded. Please try again later.")
    {
        return new()
        {
            Status = ResultStatus.TooManyRequests,
            Message = message
        };
    }
}