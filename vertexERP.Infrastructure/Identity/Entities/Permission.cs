using VertexERP.Domain.Common;

namespace VertexERP.Infrastructure.Identity.Entities
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
