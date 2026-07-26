namespace VertexERP.Application.Types.Authentication.Contracts;

public sealed class AuthenticatedUser
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Portal { get; init; }
    public required IReadOnlyList<string> Roles { get; init; }
    public required IReadOnlySet<string> Permissions { get; init; }
}