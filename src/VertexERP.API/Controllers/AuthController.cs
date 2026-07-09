using Mapster;
using Microsoft.AspNetCore.Mvc;
using VertexERP.API.Helper;
using VertexERP.Application.Models.Authentication;
using VertexERP.Application.Modules.Identity.Authentication.Login;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers;

public class AuthController : AppControllerBase
{
    [HttpPost("Login")]
    [ProducesResponseType(typeof(Result<AccessTokenResponse>), StatusCodes.Status200OK)]
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

        var accessTokenResponse = response.Data.Adapt<AccessTokenResponse>();

        var result = Result<AccessTokenResponse>.Success(accessTokenResponse);

        return ApiResponse(result);
    }

}

