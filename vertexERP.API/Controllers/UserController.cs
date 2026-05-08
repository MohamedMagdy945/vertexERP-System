using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Identity.Login;
using VertexERP.Application.Identity.Logout;
using VertexERP.Application.Identity.RefershToken;
using VertexERP.Application.Identity.Register;

namespace VertexERP.API.Controllers
{
    [ApiVersion("1.0")]
    public class UserController : AppControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Login(LogoutUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


    }
}
