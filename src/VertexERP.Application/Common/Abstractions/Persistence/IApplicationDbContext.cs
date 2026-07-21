using Microsoft.EntityFrameworkCore;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Common.Abstractions.Persistence;

public interface IApplicationDbContext
{
    // Identity
    DbSet<User> Users { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<Role> Roles { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<RolePermission> RolePermissions { get; }

    // Inventory
    public DbSet<Product> Products { get; }
    public DbSet<ProductImage> ProductImages { get; }
    public DbSet<Category> Categories { get; }
    public DbSet<Unit> Units { get; }
    public DbSet<Warehouse> Warehouses { get; }
    public DbSet<Stock> Stocks { get; }
    public DbSet<WarehouseTransaction> WarehouseTransactions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

