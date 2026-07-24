using Microsoft.EntityFrameworkCore;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;
using VertexERP.Shared.Constant;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder
{
    public sealed class RoleSeeder(ApplicationDbContext dbContext) : IDataSeeder
    {
        public int Order => 2;

        public async Task SeedAsync()
        {
            if (await dbContext.Roles.AnyAsync())
                return;

            var roles = Roles.GetAll().Select(roleName => new Role(roleName));

            await dbContext.Roles.AddRangeAsync(roles);

            await dbContext.SaveChangesAsync();
        }
    }
}
