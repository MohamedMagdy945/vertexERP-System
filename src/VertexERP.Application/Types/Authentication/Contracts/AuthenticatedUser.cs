namespace VertexERP.Application.Types.Authentication.Contracts;

public sealed record AuthenticatedUser(
    Guid Id,
    string Email,
    string Portal,
    IReadOnlyCollection<string> Roles);
