using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;
using VertexERP.Shared.Constant;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder;

public sealed class PermissionSeeder(IApplicationDbContext dbContext) : IDataSeeder
{
    public int Order => 3;
    public async Task SeedAsync()
    {
        if (await dbContext.Permissions.AnyAsync())
            return;

        var permissions = Permissions.GetAll()
            .Select(permission => new Permission(permission));

        await dbContext.Permissions.AddRangeAsync(permissions);

        await dbContext.SaveChangesAsync();
    }
}


