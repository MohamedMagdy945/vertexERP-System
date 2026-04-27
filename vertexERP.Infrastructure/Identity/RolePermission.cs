namespace VertexERP.Infrastructure.Identity
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public ApplicationRole Role { get; set; } = null!;
        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;

    }
}
