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
            new Permission { Name = Permissions.Users.View,  },
            new Permission { Name = Permissions.Users.Create },
            new Permission { Name = Permissions.Users.Update },
            new Permission { Name = Permissions.Users.Delete},
            new Permission { Name = Permissions.Products.View},
            new Permission { Name = Permissions.Products.Create },
            new Permission { Name = Permissions.Products.Update  },
            new Permission { Name = Permissions.Products.Delete }
        };

        await _dbcontext.Permissions.AddRangeAsync(permissions);
        await _dbcontext.SaveChangesAsync();
    }
}
