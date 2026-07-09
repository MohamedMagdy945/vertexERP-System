using Mapster;
using Microsoft.AspNetCore.Mvc;
using VertexERP.API.Helper;
using VertexERP.Application.Models.Authentication;
using VertexERP.Application.Modules.Identity.Authentication.Login;
using VertexERP.Application.Modules.Identity.Authentication.Logout;
using VertexERP.Application.Modules.Identity.Authentication.CreateUser;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers;

public class AuthController : AppControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<AuthenticationResponse>), StatusCodes.Status200OK)]
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

        var authenticationResponse = response.Data.Adapt<AuthenticationResponse>();

        var result = Result<AuthenticationResponse>.Success(authenticationResponse);

        return ApiResponse(result);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(Result<AuthenticationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Refresh()
    {

        var refreshToken = Request.Cookies[CookieHelper.RefreshTokenCookieName];

        var result = await Mediator.Send(new RefreshCommand(refreshToken));

        if (!result.IsSuccess || result.Data is null)
            return ApiResponse(result);

        CookieHelper.SetRefreshTokenCookie(
            Response,
            result.Data.RefreshToken,
            result.Data.RefreshTokenExpiration,
            Request.IsHttps);

        return ApiResponse(
            Result<AuthenticationResponse>.Success(
                result.Data.Adapt<AuthenticationResponse>()));
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

