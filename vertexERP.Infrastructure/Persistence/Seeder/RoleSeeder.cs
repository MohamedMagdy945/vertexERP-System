using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Authorization;
using VertexERP.Infrastructure.Identity.Entities;
using VertexERP.Infrastructure.Persistence.DbContext;
using VertexERP.Infrastructure.Persistence.SeederRunner;

namespace VertexERP.Infrastructure.Persistence.Seeder
{
    public class RoleSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;
        public RoleSeeder(ApplicationDbContext _contex)
        {
            _context = _contex;
        }

        public async Task SeedAsync()
        {
            if (await _context.Roles.AnyAsync()) return;

            var roleNames = AppRoles.GetAll();

            var roles = roleNames.Select(r => new ApplicationRole
            {
                Name = r,
                NormalizedName = r,
            });

            await _context.Roles.AddRangeAsync(roles);
            await _context.SaveChangesAsync();
        }
    }
}
