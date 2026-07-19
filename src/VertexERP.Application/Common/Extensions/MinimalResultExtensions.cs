using Microsoft.AspNetCore.Http;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Common.Extensions;

public static class MinimalResultExtensions
{
    public static IResult ToMinimalResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Success => Results.Ok(result.Data),
            ResultStatus.Created => Results.StatusCode(StatusCodes.Status201Created),
            ResultStatus.NoContent => Results.NoContent(),
            ResultStatus.ValidationFailed => Results.BadRequest(result.Errors),
            ResultStatus.NotFound => Results.NotFound(result.Errors),
            ResultStatus.Unauthorized => Results.Unauthorized(),
            ResultStatus.Forbidden => Results.Forbid(),
            ResultStatus.Conflict => Results.Conflict(result.Errors),
            _ => Results.Problem()
        };
    }
}