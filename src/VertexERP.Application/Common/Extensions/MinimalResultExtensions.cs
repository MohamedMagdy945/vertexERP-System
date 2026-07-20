using Microsoft.AspNetCore.Http;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Common.Extensions;

public static class MinimalResultExtensions
{
    public static IResult ToMinimalResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Success => Results.Ok(result),

            ResultStatus.Created => Results.Created(string.Empty, result),

            ResultStatus.NoContent => Results.NoContent(),

            ResultStatus.ValidationFailed => Results.BadRequest(result),

            ResultStatus.Unauthorized => Results.Json(result, statusCode: StatusCodes.Status401Unauthorized),

            ResultStatus.Forbidden => Results.Json(result, statusCode: StatusCodes.Status403Forbidden),

            ResultStatus.NotFound => Results.NotFound(result),

            ResultStatus.Conflict => Results.Conflict(result),

            ResultStatus.Unprocessable => Results.UnprocessableEntity(result),

            ResultStatus.TooManyRequests => Results.Json(result, statusCode: StatusCodes.Status429TooManyRequests),

            ResultStatus.Failure => Results.Json(result, statusCode: StatusCodes.Status500InternalServerError),

            _ => Results.Problem()
        };
    }
}