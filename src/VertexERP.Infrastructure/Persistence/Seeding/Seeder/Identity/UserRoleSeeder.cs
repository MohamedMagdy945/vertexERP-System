using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder.Identity;

public sealed class UserRoleSeeder(IAppDbContext dbContext) : IDataSeeder
{
    public int Order => 3;

    public async Task SeedAsync()
    {
        if (await dbContext.UserRoles.AnyAsync())
            return;

        var users = await dbContext.Users
            .ToDictionaryAsync(x => x.Email);

        var roles = await dbContext.Roles
            .ToDictionaryAsync(x => x.Name);

        var userRoles = new List<UserRole>
        {
            new(users[Users.Security.Email].Id, roles[Roles.SecurityAdmin].Id),
            new(users[Users.Standard.Email].Id, roles[Roles.StandardUser].Id),
            new(users[Users.System.Email].Id, roles[Roles.SystemAdmin].Id),
        };

        await dbContext.UserRoles.AddRangeAsync(userRoles);
        await dbContext.SaveChangesAsync();
    }
}