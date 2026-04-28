using MediatR;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Common.Bases;

namespace VertexERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppControllerBase : ControllerBase
    {
        private IMediator? _mediatorInstance;
        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>()!;

        protected ObjectResult NewResult<T>(Response<T> response)
        {
            return response.StatusCode switch
            {
                200 => new OkObjectResult(response),
                201 => new CreatedResult(string.Empty, response),
                404 => new NotFoundObjectResult(response),
                400 => new BadRequestObjectResult(response),
                401 => new UnauthorizedObjectResult(response),
                422 => new UnprocessableEntityObjectResult(response),
                _ => new BadRequestObjectResult(response)
            };
        }
    }
}
