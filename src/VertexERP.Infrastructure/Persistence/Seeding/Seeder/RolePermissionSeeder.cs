using Microsoft.EntityFrameworkCore;
using System.Data;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;
using VertexERP.Shared.Constant;
namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder;

public sealed class RolePermissionSeeder(IApplicationDbContext dbContext) : IDataSeeder
{
    public int Order => 5;

    public async Task SeedAsync()
    {
        if (await dbContext.Permissions.AnyAsync())
            return;
        var roles = await dbContext.Roles
            .Where(role => Roles.GetAll().Contains(role.Name))
            .ToDictionaryAsync(role => role.Name);

        var permissions = await dbContext.Permissions
            .Where(permission => Permissions.GetAll().Contains(permission.Name))
            .ToDictionaryAsync(permission => permission.Name);

        var adminRole = roles[Roles.Admin];
        var userRole = roles[Roles.User];

        var rolePermissions = new List<RolePermission>();

        rolePermissions.AddRange(Permissions.GetAll()
            .Select(permission => new RolePermission(adminRole.Id, permissions[permission].Id)));

        rolePermissions.Add(
            new RolePermission(
                userRole.Id,
                permissions[Permissions.Products.Read].Id));

        await dbContext.RolePermissions.AddRangeAsync(
            rolePermissions);

        await dbContext.SaveChangesAsync();
    }
}