namespace VertexERP.Application.Types.Authentication.Contracts;

public sealed class AuthenticatedUser
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string Portal { get; set; } = default!;
    public IReadOnlyCollection<string> Roles { get; set; } = [];
    public IReadOnlySet<string> Permissions { get; set; } = new HashSet<string>();
}