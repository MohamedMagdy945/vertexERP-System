namespace VertexERP.Application.Types.Authentication.Models;

public sealed record RefreshTokenInfo(
    string Token,
    DateTime ExpiresAt
);