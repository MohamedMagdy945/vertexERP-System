using Mapster;
using Microsoft.AspNetCore.Mvc;
using VertexERP.API.Helper;
using VertexERP.Application.Models.Authentication;
using VertexERP.Application.Modules.Identity.Authentication.Login;
using VertexERP.Application.Modules.Identity.Authentication.Logout;
using VertexERP.Application.Modules.Identity.Authentication.Refresh;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Identity;


[Tags("Identity")]
public class AuthenticationController : AppControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<AuthenticationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginCommand command)
    {

        var response = Result<AuthenticationResponse>.Create();

        var result = await Mediator.Send(command);

        if (!result.IsSuccess || result.Data is null)
            return ApiResponse(result);

        CookieHelper.SetRefreshTokenCookie(
            Response,
            result.Data.RefreshToken,
            result.Data.RefreshTokenExpiration,
            HttpContext.Request.IsHttps
        );

        var authenticationResponse = result.Data.Adapt<AuthenticationResponse>();

        return ApiResponse(response.Success(authenticationResponse));
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(Result<AuthenticationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Refresh()
    {
        var response = Result<AuthenticationResponse>.Create();

        var refreshToken = Request.Cookies[CookieHelper.RefreshTokenCookieName];

        var result = await Mediator.Send(new RefreshCommand(refreshToken!));

        if (!result.IsSuccess || result.Data is null)
            return ApiResponse(result);

        CookieHelper.SetRefreshTokenCookie(
            Response,
            result.Data.RefreshToken,
            result.Data.RefreshTokenExpiration,
            Request.IsHttps);

        var authenticationResponse = result.Data.Adapt<AuthenticationResponse>();

        return ApiResponse(response.Success(authenticationResponse));
    }

    [HttpPost("logout")]
    [ProducesResponseType(typeof(Result<LogoutResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies[CookieHelper.RefreshTokenCookieName] ?? string.Empty;

        var result = await Mediator.Send(new LogoutCommand(refreshToken));

        CookieHelper.DeleteRefreshTokenCookie(Response, HttpContext.Request.IsHttps);

        return ApiResponse(result);
    }

}

