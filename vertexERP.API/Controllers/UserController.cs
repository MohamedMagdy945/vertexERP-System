using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Identity.Register;

namespace VertexERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
