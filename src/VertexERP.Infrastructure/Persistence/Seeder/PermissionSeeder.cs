using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Shared.Constants;

namespace VertexERP.Infrastructure.Persistence.Seeder;

public class PermissionSeeder
{
    private readonly IApplicationDbContext _dbcontext;

    public PermissionSeeder(
        IApplicationDbContext dbcontext
     )
    {
        _dbcontext = dbcontext;
    }

    public async Task SeedAsync()
    {
        if (await _dbcontext.Permissions.AnyAsync())
            return;

        var permissions = new List<Permission>
        {
            new Permission { Name = PermissionNames.Users.View,  },
            new Permission { Name = PermissionNames.Users.Create },
            new Permission { Name = PermissionNames.Users.Update },
            new Permission { Name = PermissionNames.Users.Delete},
            new Permission { Name = PermissionNames.Products.View},
            new Permission { Name = PermissionNames.Products.Create },
            new Permission { Name = PermissionNames.Products.Update  },
            new Permission { Name = PermissionNames.Products.Delete },
            new Permission { Name = PermissionNames.Permissions.View },
            new Permission { Name = PermissionNames.Permissions.Update },

        };

        await _dbcontext.Permissions.AddRangeAsync(permissions);
        await _dbcontext.SaveChangesAsync();
    }
}
