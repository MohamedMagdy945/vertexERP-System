using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Authorization;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder;

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
            new(
                users[SeedUsers.AdminEmail].Id,
                roles[SecurityRoles.Admin].Id),

            new(
                users[SeedUsers.SystemEmail].Id,
                roles[SecurityRoles.SystemAdmin].Id),

            new(
                users[SeedUsers.SecurityEmail].Id,
                roles[SecurityRoles.SecurityAdmin].Id),

            new(
                users[SeedUsers.UserEmail].Id,
                roles[SecurityRoles.User].Id)
        };

        await dbContext.UserRoles.AddRangeAsync(userRoles);
        await dbContext.SaveChangesAsync();
    }
}