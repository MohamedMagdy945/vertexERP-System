using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Identity.Login;
using VertexERP.Application.Identity.Logout;
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

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Login(LogoutUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }


    }
}
