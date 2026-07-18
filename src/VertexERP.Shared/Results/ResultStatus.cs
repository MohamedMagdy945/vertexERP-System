namespace VertexERP.Shared.Results;


public enum ResultStatus
{
    Success = 0,
    Created = 1,
    Failure = 2,
    ValidationFailed = 3,
    NotFound = 4,
    Unauthorized = 5,
    Forbidden = 6,
    Conflict = 7,
    NoContent = 8
}