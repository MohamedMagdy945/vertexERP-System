using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Abstractions.Authentication;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Shared.Constants;

namespace VertexERP.Infrastructure.Persistence.Seeder;

public class UserSeeder
{
    private readonly IApplicationDbContext _dbcontext;
    private readonly IPasswordHasher _passwordHasher;

    public UserSeeder(
        IApplicationDbContext dbcontext,
        IPasswordHasher passwordHasher)
    {
        _dbcontext = dbcontext;
        _passwordHasher = passwordHasher;
    }

    public async Task SeedAsync()
    {
        if (await _dbcontext.Users.AnyAsync())
            return;

        var permissions = await _dbcontext.Permissions.ToListAsync();

        var admin = new User
        {
            FirstName = "System",
            LastName = "Admin",
            FullName = "admin",
            Email = "admin@gmail.com",
            PasswordHash = _passwordHasher.Hash("Admin@123"),
            IsEnabled = true
        };

        var user = new User
        {
            FirstName = "Test",
            LastName = "User",
            FullName = "user",
            Email = "user@gmail.com",
            PasswordHash = _passwordHasher.Hash("User@123"),
            IsEnabled = true
        };

        admin.UserPermissions = permissions
            .Select(p => new UserPermission
            {
                PermissionId = p.Id
            })
            .ToList();

        user.UserPermissions =
        [
            new UserPermission
        {
            PermissionId = permissions.First(p => p.Name == Permissions.Users.View).Id
        },
        new UserPermission
        {
            PermissionId = permissions.First(p => p.Name == Permissions.Products.View).Id
        }
        ];

        await _dbcontext.Users.AddRangeAsync(admin, user);
        await _dbcontext.SaveChangesAsync();
    }
}

