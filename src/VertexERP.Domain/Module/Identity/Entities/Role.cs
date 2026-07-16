using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Identity.Entities;

public class Role : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public ICollection<UserRole> UserRoles { get; } = [];
    public ICollection<RolePermission> RolePermissions { get; } = [];
    private Role() { }

    public Role(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public void AddPermission(RolePermission rolePermission)
    {
        if (!RolePermissions.Contains(rolePermission))
        {
            RolePermissions.Add(rolePermission);
        }
    }
    public void RemovePermission(RolePermission rolePermission)
    {
        RolePermissions.Remove(rolePermission);
    }
}
