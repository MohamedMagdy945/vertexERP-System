using Microsoft.EntityFrameworkCore;
using VertexERP.Domain.Module.Catalog.Entities;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Domain.Module.Notifications.Entities;
namespace VertexERP.Application.Common.Abstractions.Persistence;

public interface IAppDbContext
{
    // Identity
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<RolePermission> RolePermissions { get; }


    // Catalog
    DbSet<Product> Products { get; }
    DbSet<ProductImage> ProductImages { get; }
    DbSet<Category> Categories { get; }
    DbSet<MeasurementUnit> MeasurementUnits { get; }

    // Inventory
    DbSet<Warehouse> Warehouses { get; }
    DbSet<Stock> Stocks { get; }
    DbSet<StockMovement> StockMovements { get; }

    // Notifications
    DbSet<Notification> Notifications { get; }
    DbSet<NotificationRecipient> NotificationRecipients { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


}

