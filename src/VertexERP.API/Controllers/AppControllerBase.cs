using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class AppControllerBase(ISender sender) : ControllerBase
{
    protected ISender Sender { get; } = sender;

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Success => Ok(result.Data),

            ResultStatus.Created => StatusCode(StatusCodes.Status201Created, result.Data),

            ResultStatus.NoContent => NoContent(),

            ResultStatus.ValidationFailed => BadRequest(result.Errors),

            ResultStatus.NotFound => NotFound(result.Errors),

            ResultStatus.Unauthorized => Unauthorized(result.Errors),

            ResultStatus.Forbidden => Forbid(),

            ResultStatus.Conflict => Conflict(result.Errors),

            _ => Problem()
        };
    }
}