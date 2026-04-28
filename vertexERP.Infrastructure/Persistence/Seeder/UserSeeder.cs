using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Authorization;
using VertexERP.Infrastructure.Identity.Entities;
using VertexERP.Infrastructure.Persistence.DbContext;
using VertexERP.Infrastructure.Persistence.SeederRunner;

namespace VertexERP.Infrastructure.Persistence.Seeder
{
    public class UserSeeder : IDataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public UserSeeder(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            if (await _context.Users.AnyAsync())
                return;

            var adminRole = await _roleManager.FindByNameAsync(AppRoles.Admin);
            if (adminRole == null)
                return;

            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@test.com",
            };

            var result = await _userManager.CreateAsync(admin, "Admin@123");

            if (!result.Succeeded)
                return;

            await _userManager.AddToRoleAsync(admin, AppRoles.Admin);
        }
    }
}

