using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Common.Types.Authentication.Contracts;

public sealed record TokenPair(
    AccessTokenInfo AccessToken,
    RefreshTokenInfo RefreshToken);