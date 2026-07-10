using Microsoft.EntityFrameworkCore;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<RefreshToken> RefreshTokens { get; }
    public DbSet<Permission> Permissions { get; }
    public DbSet<UserPermission> UserPermissions { get; }

    public DbSet<Product> Products { get; }
    public DbSet<Category> Categories { get; }
    public DbSet<Unit> Units { get; }
    public DbSet<Stock> Stocks { get; }
    public DbSet<Warehouse> Warehouses { get; }
    public DbSet<StockMovement> StockMovements { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

