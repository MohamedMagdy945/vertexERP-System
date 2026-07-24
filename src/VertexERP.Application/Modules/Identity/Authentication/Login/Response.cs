namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed record Response(string AccessToken, DateTime AccessTokenExpiresAt
                            , string RefreshToken, DateTime RefreshTokenExpiresAt
);