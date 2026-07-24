using Microsoft.EntityFrameworkCore;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;
using VertexERP.Shared.Constant;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder
{
    public sealed class UserRoleSeeder(ApplicationDbContext dbContext) : IDataSeeder
    {
        public int Order => 4;

        public async Task SeedAsync()
        {
            if (await dbContext.UserRoles.AnyAsync())
                return;

            var users = await dbContext.Users.Where(user => Users.GetAll().Contains(user.FullName)).ToDictionaryAsync(user => user.FullName);

            var roles = await dbContext.Roles.Where(role => Roles.GetAll().Contains(role.Name)).ToDictionaryAsync(role => role.Name);

            await dbContext.UserRoles.AddRangeAsync(
                new UserRole(users[Users.Admin].Id, roles[Roles.Admin].Id),
                new UserRole(users[Users.System].Id, roles[Roles.Admin].Id),
                new UserRole(users[Users.User].Id, roles[Roles.User].Id));

            await dbContext.SaveChangesAsync();
        }
    }
}
