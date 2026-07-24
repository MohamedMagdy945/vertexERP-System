using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;
using VertexERP.Shared.Constant;

namespace VertexERP.Infrastructure.Persistence.Seeding.Seeder
{
    public sealed class UserSeeder(
     ApplicationDbContext dbContext,
     IPasswordHasher passwordHasher) : IDataSeeder
    {
        public int Order => 1;
        public async Task SeedAsync()
        {
            if (await dbContext.Users.AnyAsync())
                return;

            var admin = new User(Users.Admin, "admin@example.com", passwordHasher.Hash("Admin@123"));

            var system = new User(Users.System, "system@example.com", passwordHasher.Hash("System@123"));

            var user = new User(Users.User, "user@example.com", passwordHasher.Hash("User@123"));

            await dbContext.Users.AddRangeAsync(admin, system, user);

            await dbContext.SaveChangesAsync();
        }
    }
}

