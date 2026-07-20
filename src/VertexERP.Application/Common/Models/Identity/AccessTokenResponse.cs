namespace VertexERP.Application.Common.Models.Identity;

public record AccessTokenResponse(
    string AccessToken,
    DateTime AccessTokenExpiresAt
);