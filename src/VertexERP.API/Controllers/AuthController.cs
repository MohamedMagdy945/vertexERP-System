using Mapster;
using Microsoft.AspNetCore.Mvc;
using VertexERP.API.Helper;
using VertexERP.Application.Modules.Identity.Authentication.Login;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers;

public class AuthController : AppControllerBase
{
    [HttpPost("Login")]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var response = await Mediator.Send(command);

        if (!response.IsSuccess || response.Data is null)
            return ApiResponse(response);

        CookieHelper.SetRefreshTokenCookie(
            Response,
            response.Data.RefreshToken,
            response.Data.RefreshTokenExpiration,
            HttpContext.Request.IsHttps
        );

        var loginResponse = response.Data.Adapt<LoginResponse>();

        var result = Result<LoginResponse>.Success(loginResponse);

        return ApiResponse(result);
    }
    [HttpGet("test")]
    public IActionResult Test()
    {
        throw new Exception("Test");
    }
}

