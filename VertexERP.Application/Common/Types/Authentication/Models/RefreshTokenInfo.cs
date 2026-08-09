namespace VertexERP.Application.Common.Types.Authentication.Models;

public sealed record RefreshTokenInfo(
    string Token,
    DateTime ExpiresAt
);