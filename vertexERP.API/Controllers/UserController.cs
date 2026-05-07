using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Identity.Login;
using VertexERP.Application.Identity.RefershToken;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("RefershToken")]
        public async Task<IActionResult> Login(RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

    }
}
