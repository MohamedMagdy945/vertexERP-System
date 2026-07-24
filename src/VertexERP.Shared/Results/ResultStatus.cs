namespace VertexERP.Shared.Results;

public enum ResultStatus
{
    Success = 0,          // 200
    Created = 1,          // 201
    NoContent = 2,        // 204

    BadRequest = 3,       // 400 
    ValidationFailed = 4, // 400
    Unauthorized = 5,     // 401
    Forbidden = 6,        // 403
    NotFound = 7,         // 404
    Conflict = 8,         // 409

    Failure = 9,          // 500
    Unprocessable = 10,   // 422
    TooManyRequests = 11  // 429
}