using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vertexERP.Application.Interfaces;
using vertexERP.Domain.Modules.Auth;
using vertexERP.Infrastructure.Identity;

namespace vertexERP.Infrastructure.Persistence
{
    public class ApplicationDbContext :
        IdentityDbContext<ApplicationUser, ApplicationRole, int>,
        IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }


    }
}
