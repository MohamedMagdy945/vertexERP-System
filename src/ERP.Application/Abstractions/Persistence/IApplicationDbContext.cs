using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }

    public DbSet<User> Users { get; }
    public DbSet<RefreshToken> RefreshTokens { get; }
    public DbSet<Permission> Permissions { get; }
    public DbSet<UserPermission> UserPermissions { get; }

    public DbSet<Product> Products { get; }
    public DbSet<ProductImage> ProductImages { get; }
    public DbSet<Category> Categories { get; }
    public DbSet<Stock> Stocks { get; }
    public DbSet<Warehouse> Warehouses { get; }
    public DbSet<InventoryTransaction> InventoryTransactions { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

