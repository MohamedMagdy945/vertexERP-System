using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Common.Extensions;

public static class CookieExtensions
{
    public const string RefreshTokenCookieName = "refreshToken";
    private const string CookiePath = "/api/authentication";

    public static void SetRefreshTokenCookie(
        this HttpResponse response,
        string refreshToken,
        DateTime expires,
        bool isSecure = true)
    {
        response.Cookies.Append(RefreshTokenCookieName, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = isSecure,
            SameSite = SameSiteMode.Lax,
            Expires = expires,
            Path = CookiePath
        });
    }

    public static void DeleteRefreshTokenCookie(
        this HttpResponse response,
        bool isSecure = true)
    {
        response.Cookies.Delete(RefreshTokenCookieName, new CookieOptions
        {
            HttpOnly = true,
            Secure = isSecure,
            SameSite = SameSiteMode.Lax,
            Path = CookiePath
        });
    }
}