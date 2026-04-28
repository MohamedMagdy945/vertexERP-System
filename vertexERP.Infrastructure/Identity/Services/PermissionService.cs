using Microsoft.EntityFrameworkCore;
using VertexERP.Infrastructure.Persistence.DbContext;

namespace VertexERP.Infrastructure.Identity.Identity
{
    public class PermissionService
    {
        private readonly ApplicationDbContext _context;

        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetRolePermissionsAsync(string roleName)
        {

            var permissions = await _context.RolePermissions
           .Where(x => x.Role.Name == roleName)
           .Select(x => x.Permission.Name)
           .Distinct()
           .ToListAsync();

            return permissions;
        }
        public async Task<List<string>> GetUserPermissionsAsync(int userId)
        {

            var permissions = await (
                 from ur in _context.UserRoles
                 where ur.UserId == userId
                 join rp in _context.RolePermissions on ur.RoleId equals rp.RoleId
                 join p in _context.Permissions on rp.PermissionId equals p.Id
                 select p.Name
             )
             .Distinct()
             .ToListAsync();

            return permissions;
        }
    }
}
