namespace VertexERP.Application.Types.Authentication.Contracts;

public sealed record AuthenticationResult(
    AuthenticatedUser User,
    TokenPair TokenPair);
