using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Security;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder.Identity;

public sealed class RolePermissionSeeder(IAppDbContext dbContext) : IDataSeeder
{
    public int Order => 5;

    public async Task SeedAsync()
    {
        if (await dbContext.RolePermissions.AnyAsync())
            return;

        var securityRole = await dbContext.Roles
            .SingleAsync(x => x.Name == SecurityRoles.SecurityAdmin);

        var rolePermissions = new[]
        {
            new RolePermission(securityRole.Id, SecurityPerms.Identity.View),
            new RolePermission(securityRole.Id, SecurityPerms.Identity.Manage)
        };

        await dbContext.RolePermissions.AddRangeAsync(rolePermissions);
        await dbContext.SaveChangesAsync();


    }
}