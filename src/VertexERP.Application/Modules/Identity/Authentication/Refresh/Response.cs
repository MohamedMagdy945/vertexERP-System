namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed record Response(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt,
    DateTime RefreshTokenExpiresAt
);