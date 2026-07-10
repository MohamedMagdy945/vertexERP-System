namespace VertexERP.API.Helper;

public static class CookieHelper
{
    public const string RefreshTokenCookieName = "refreshToken";
    private const string CookiePath = "/api/authentication";

    public static void SetRefreshTokenCookie(
        HttpResponse response,
        string refreshToken,
        DateTime expires,
        bool isSecure = true)
    {
        response.Cookies.Append(RefreshTokenCookieName, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = expires,
            Path = CookiePath
        });
    }

    public static void DeleteRefreshTokenCookie(HttpResponse response, bool isSecure = true)
    {
        response.Cookies.Delete(RefreshTokenCookieName, new CookieOptions
        {
            HttpOnly = true,
            Secure = isSecure,
            SameSite = SameSiteMode.Strict,
            Path = CookiePath
        });
    }
}

