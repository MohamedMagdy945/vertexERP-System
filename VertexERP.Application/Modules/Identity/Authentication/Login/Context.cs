namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed class Context
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string PasswordHash { get; init; } = null!;
    public bool IsActive { get; init; }
    public string? AvatarUrl { get; init; }
    public string Portal { get; init; } = null!;
    public List<string> Roles { get; init; } = [];
}