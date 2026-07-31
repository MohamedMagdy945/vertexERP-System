namespace VertexERP.Application.Common.Types.Authentication.Contracts;

public sealed record AuthenticationResult(
    AuthenticatedUser User,
    TokenPair TokenPair);
