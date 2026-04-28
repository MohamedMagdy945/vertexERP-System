using Microsoft.AspNetCore.Identity;

namespace VertexERP.Infrastructure.Identity.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
