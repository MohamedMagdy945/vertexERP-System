namespace VertexERP.Application.Modules.Identity.Users.GetById;

public sealed record RoleResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
public sealed class Response
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
    public string PortalType { get; set; } = default!;
    public bool IsEmailConfirmed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public IReadOnlyList<RoleResponse> Roles { get; set; } = [];
    public IReadOnlySet<string> Permissions { get; set; } = new HashSet<string>();
}
