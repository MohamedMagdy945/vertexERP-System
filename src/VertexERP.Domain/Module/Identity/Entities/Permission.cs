namespace VertexERP.Domain.Module.Identity.Entities;

public class Permission : BaseIdentityEntity
{
    public string Name { get; set; } = null!;
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}

