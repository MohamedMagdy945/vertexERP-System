using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Authorization;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder
{
    public sealed class RoleSeeder(AppDbContext dbContext) : IDataSeeder
    {
        public int Order => 2;

        public async Task SeedAsync()
        {
            if (await dbContext.Roles.AnyAsync())
                return;

            var roles = SecurityRoles.All().Select(roleName => new Role(roleName));

            await dbContext.Roles.AddRangeAsync(roles);

            await dbContext.SaveChangesAsync();
        }
    }
}
