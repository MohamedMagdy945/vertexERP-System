using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder
{
    public sealed class UserSeeder(
    IAppDbContext dbContext,
    IPasswordHasher passwordHasher) : IDataSeeder
    {
        public int Order => 1;

        public async Task SeedAsync()
        {
            if (await dbContext.Users.AnyAsync())
                return;

            await dbContext.Users.AddRangeAsync(
                new User(
                    SeedUsers.AdminName,
                    SeedUsers.AdminEmail,
                    passwordHasher.Hash(SeedUsers.AdminPassword)),

                new User(
                    SeedUsers.SystemName,
                    SeedUsers.SystemEmail,
                    passwordHasher.Hash(SeedUsers.SystemPassword)),

                new User(
                    SeedUsers.UserName,
                    SeedUsers.UserEmail,
                    passwordHasher.Hash(SeedUsers.UserPassword)),

                new User(
                    SeedUsers.SecurityName,
                    SeedUsers.SecurityEmail,
                    passwordHasher.Hash(SeedUsers.SecurityPassword)));

            await dbContext.SaveChangesAsync();
        }
    }
}

public static class SeedUsers
{
    public const string AdminName = "Admin";
    public const string AdminEmail = "admin@example.com";
    public const string AdminPassword = "Admin@123";

    public const string SystemName = "System";
    public const string SystemEmail = "system@example.com";
    public const string SystemPassword = "System@123";

    public const string SecurityName = "Security";
    public const string SecurityEmail = "security@example.com";
    public const string SecurityPassword = "Security@123";

    public const string UserName = "User";
    public const string UserEmail = "user@example.com";
    public const string UserPassword = "User@123";
}

