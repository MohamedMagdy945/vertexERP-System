using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Catalog.Entities;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Domain.Module.Notifications.Entities;

namespace VertexERP.Application.Tests.Modules;

public class TestAppDbContext : DbContext, IAppDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles => throw new NotImplementedException();

    public DbSet<RefreshToken> RefreshTokens => throw new NotImplementedException();

    public DbSet<UserRole> UserRoles => throw new NotImplementedException();

    public DbSet<RolePermission> RolePermissions => throw new NotImplementedException();

    public DbSet<Product> Products => throw new NotImplementedException();

    public DbSet<ProductImage> ProductImages => throw new NotImplementedException();

    public DbSet<Category> Categories => throw new NotImplementedException();

    public DbSet<MeasurementUnit> MeasurementUnits => throw new NotImplementedException();

    public DbSet<Warehouse> Warehouses => throw new NotImplementedException();

    public DbSet<Stock> Stocks => throw new NotImplementedException();

    public DbSet<StockMovement> StockMovements => throw new NotImplementedException();

    public DbSet<Notification> Notifications => throw new NotImplementedException();

    public DbSet<NotificationRecipient> NotificationRecipients => throw new NotImplementedException();

    public DbSet<StockAdjustment> StockAdjustments => throw new NotImplementedException();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }
}