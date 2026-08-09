using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder.Identity;

public sealed class UserSeeder(
IAppDbContext dbContext,
IPasswordHasher passwordHasher) : IDataSeeder
{
    public int Order => 1;

    public async Task SeedAsync()
    {
        if (await dbContext.Users.AnyAsync())
            return;

        var users = Users.
            All.
            Select(seed => new User(seed.Name, seed.Email, passwordHasher.Hash(seed.Password)))
            .ToList();

        await dbContext.Users.AddRangeAsync(users);

        await dbContext.SaveChangesAsync();
    }
}

public static class Users
{
    public static readonly UserSeed Administrator =
        new("Administrator", "admin@example.com", "Admin@123");

    public static readonly UserSeed System =
        new("System", "system@example.com", "System@123");

    public static readonly UserSeed Security =
        new("Security", "security@example.com", "Security@123");

    public static readonly UserSeed Standard =
        new("User", "user@example.com", "User@123");

    public static IReadOnlyCollection<UserSeed> All =>
    [
        Administrator,
        System,
        Security,
        Standard
    ];
}
public sealed record UserSeed(
    string Name,
    string Email,
    string Password);
