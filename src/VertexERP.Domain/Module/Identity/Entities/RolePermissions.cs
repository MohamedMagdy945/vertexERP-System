namespace VertexERP.Domain.Module.Identity.Entities;

public class RolePermission
{
    public Guid RoleId { get; private set; }
    public string Permission { get; private set; } = default!;

    public Role Role { get; private set; } = default!;
    private RolePermission() { }

    public RolePermission(Guid roleId, string permission)
    {
        RoleId = roleId;
        Permission = permission;
    }
}