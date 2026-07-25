using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Types.Authentication.Contracts;

public sealed record TokenPair(
    AccessTokenInfo AccessToken,
    RefreshTokenInfo RefreshToken);