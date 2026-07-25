namespace VertexERP.Application.Types.Authentication.Models;

public sealed record AccessTokenInfo(
    string Token,
    DateTime ExpiresAt
);

