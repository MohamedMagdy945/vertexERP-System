namespace VertexERP.Application.Common.Types.Authentication.Contracts;

public sealed record AuthenticatedUser(
    Guid Id,
    string FullName,
    string Email,
    string? AvatarUrl,
    string Portal,
    IReadOnlyList<string> Roles,
    IReadOnlySet<string> Permissions);