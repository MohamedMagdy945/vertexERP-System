using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Types.Authentication.Contracts;

public sealed record AuthenticationResult(
    AuthenticatedUser User,
    AccessTokenInfo AccessToken,
    RefreshTokenInfo RefreshToken);
