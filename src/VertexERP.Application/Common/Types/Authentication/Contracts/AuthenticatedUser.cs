namespace VertexERP.Application.Common.Types.Authentication.Contracts;

public sealed class AuthenticatedUser
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public string? AvatarUrl { get; init; }
    public required string Portal { get; init; }
    public IReadOnlyList<string> Roles { get; init; } = [];
    public IReadOnlySet<string> Permissions { get; init; } = new HashSet<string>();
}
