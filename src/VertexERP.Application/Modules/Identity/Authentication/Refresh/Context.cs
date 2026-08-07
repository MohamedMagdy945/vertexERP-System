using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed class Context
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public bool IsActive { get; init; }
    public string? AvatarUrl { get; init; }
    public required string Portal { get; init; }
    public required IReadOnlyList<string> Roles { get; init; }
    public required RefreshToken RefreshToken { get; init; }
}