using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Shared.Constant;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder;

public sealed class UserRoleSeeder(ApplicationDbContext dbContext) : IDataSeeder
{
    public int Order => 4;

    public async Task SeedAsync()
    {
        if (await dbContext.UserRoles.AnyAsync())
            return;

        var users = await dbContext.Users.Where(user => SystemUsers.All().Contains(user.Name)).ToDictionaryAsync(user => user.Name);

        var roles = await dbContext.Roles.Where(role => Roles.All().Contains(role.Name)).ToDictionaryAsync(role => role.Name);

        await dbContext.UserRoles.AddRangeAsync(
            new UserRole(users[SystemUsers.Security].Id, roles[Roles.SecurityAdmin].Id),
            new UserRole(users[SystemUsers.Security].Id, roles[Roles.Admin].Id),
            new UserRole(users[SystemUsers.Admin].Id, roles[Roles.Admin].Id),
            new UserRole(users[SystemUsers.System].Id, roles[Roles.SystemAdmin].Id),
            new UserRole(users[SystemUsers.System].Id, roles[Roles.Admin].Id),
            new UserRole(users[SystemUsers.User].Id, roles[Roles.User].Id));

        await dbContext.SaveChangesAsync();
    }
}

