namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed class Context
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = default!;
    public string PasswordHash { get; init; } = default!;
    public bool IsActive { get; init; }
    public string PortalType { get; init; } = default!;
    public IReadOnlyCollection<string> Roles { get; init; } = [];
}