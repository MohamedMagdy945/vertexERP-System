namespace VertexERP.Application.Modules.Identity.Roles.GetById;

public sealed class RoleResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
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
