namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed record Response(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt,
    DateTime RefreshTokenExpiresAt
);