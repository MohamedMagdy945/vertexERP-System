using vertexERP.Domain.Common;

namespace vertexERP.Infrastructure.Identity
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
