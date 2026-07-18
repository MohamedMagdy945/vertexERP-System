namespace VertexERP.Application.Common.Models.Identity;

public record TokenPair(
    string AccessToken,
    DateTime AccessTokenExpiresAt,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt,
    string RefreshTokenHash
    );