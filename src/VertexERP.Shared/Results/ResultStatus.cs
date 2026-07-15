namespace VertexERP.Shared.Results;

public enum ResultStatus
{
    Success = 0,
    Failure = 1,
    Validation = 2,
    NotFound = 3,
    Unauthorized = 4,
    Forbidden = 5,
    Conflict = 6,
    NoContent = 7,
}