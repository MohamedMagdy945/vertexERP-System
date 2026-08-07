namespace VertexERP.Application.Common.Types.Authentication.Models;

public sealed record AccessTokenInfo(
    string Token,
    DateTime ExpiresAt
);

