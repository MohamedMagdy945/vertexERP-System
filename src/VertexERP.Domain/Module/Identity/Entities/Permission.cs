using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Identity.Entities;

public class Permission : Entity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public ICollection<RolePermission> RolePermissions { get; } = [];

    private Permission() { }

    public Permission(string name, string? description = null)
    {
        Name = name;
        Description = description;
    }
    public void UpdateDescription(string description)
    {
        Description = description;
    }
}
