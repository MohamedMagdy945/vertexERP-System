namespace VertexERP.Application.Common.Models.Identity;

public record TokenPair(
    string AccessToken,
    DateTime AccessTokenExpiresUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresUtc
);