using VertexERP.Application.Common.Types.Authentication.Models;

namespace VertexERP.Application.Common.Types.Authentication.Contracts;

public sealed record AuthenticationResult(
    AuthenticatedUser User,
    AccessTokenInfo AccessToken,
    RefreshTokenInfo RefreshToken);