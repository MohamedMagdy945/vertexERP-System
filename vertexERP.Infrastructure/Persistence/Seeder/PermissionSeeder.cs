using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Authorization;
using VertexERP.Infrastructure.Identity.Entities;
using VertexERP.Infrastructure.Persistence.DbContext;
using VertexERP.Infrastructure.Persistence.SeederRunner;
using VertexERP.Shared.Constants;

namespace VertexERP.Infrastructure.Persistence.Seeder
{
    public class PermissionSeeder : IDataSeeder
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public PermissionSeeder(
            RoleManager<ApplicationRole> roleManager,
            ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task SeedAsync()
        {
            var adminRole = await _roleManager.FindByNameAsync(AppRoles.Admin);

            if (adminRole == null) return;
            if (await _context.Permissions.AnyAsync()) return;

            var permissions = AppPermissions.GetAll()
              .Select(p => new Permission
              {
                  Name = p,
                  RolePermissions = new List<RolePermission>
                  {
                       new RolePermission
                       {
                            RoleId = adminRole.Id
                       }
                  }
              }).ToList();

            await _context.Permissions.AddRangeAsync(permissions);
            await _context.SaveChangesAsync();
        }

    }
}
