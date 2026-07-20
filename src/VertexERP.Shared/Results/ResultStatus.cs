namespace VertexERP.Shared.Results;


public enum ResultStatus
{
    Success = 0,          // 200
    Created = 1,          // 201
    NoContent = 2,        // 204

    ValidationFailed = 3, // 400
    Unauthorized = 4,     // 401
    Forbidden = 5,        // 403
    NotFound = 6,         // 404
    Conflict = 7,         // 409

    Failure = 8,          // 500
    Unprocessable = 9,    // 422
    TooManyRequests = 10  // 429
}