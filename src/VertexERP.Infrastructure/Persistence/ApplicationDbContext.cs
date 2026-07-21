using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Domain.Module.Inventory.Entities;


namespace VertexERP.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    //Identity
    public DbSet<User> Users { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    // Inventory
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<WarehouseTransaction> WarehouseTransactions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

}
