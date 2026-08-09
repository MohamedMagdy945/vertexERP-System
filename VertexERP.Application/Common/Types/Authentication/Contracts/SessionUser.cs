namespace VertexERP.Application.Common.Types.Authentication.Contracts;

public sealed class SessionUser
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public string? AvatarUrl { get; init; }
    public required string Portal { get; init; }
    public required IReadOnlyList<string> Roles { get; init; }
}