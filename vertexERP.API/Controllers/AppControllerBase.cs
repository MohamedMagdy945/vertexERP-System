using MediatR;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Common.Bases;

namespace VertexERP.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class AppControllerBase : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        protected ObjectResult NewResult<T>(Response<T> response)
        {
            return response.StatusCode switch
            {
                200 => new OkObjectResult(response),
                201 => new CreatedResult(string.Empty, response),
                400 => new BadRequestObjectResult(response),
                401 => new UnauthorizedObjectResult(response),
                404 => new NotFoundObjectResult(response),
                422 => new UnprocessableEntityObjectResult(response),
                _ => new BadRequestObjectResult(response)
            };
        }
    }
}