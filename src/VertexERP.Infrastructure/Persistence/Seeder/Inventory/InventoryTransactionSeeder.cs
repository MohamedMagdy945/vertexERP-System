using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Infrastructure.Persistence.Seeder.Inventory;

public class InventoryTransactionSeeder
{
    private readonly IApplicationDbContext _dbContext;

    public InventoryTransactionSeeder(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (await _dbContext.InventoryTransactions.AnyAsync())
            return;

        var transactions = new List<InventoryTransaction>
        {
            new()
            {
                Type = InventoryTransactionType.Receipt,
                WarehouseId = 1,
                ProductId = 1,
                Quantity = 15,
                ReceivedByUserId = 1,
                TransactionDate = DateTime.UtcNow.AddDays(-15),
                ReferenceNumber = "PO-1001",
                Notes = "Initial stock received"
            },
            new()
            {
                Type = InventoryTransactionType.Receipt,
                WarehouseId = 1,
                ProductId = 2,
                Quantity = 8,
                ReceivedByUserId = 1,
                TransactionDate = DateTime.UtcNow.AddDays(-14),
                ReferenceNumber = "PO-1002",
                Notes = "Supplier delivery"
            },
            new()
            {
                Type = InventoryTransactionType.Receipt,
                WarehouseId = 2,
                ProductId = 3,
                Quantity = 40,
                ReceivedByUserId = 2,
                TransactionDate = DateTime.UtcNow.AddDays(-13),
                ReferenceNumber = "PO-1003",
                Notes = "Warehouse replenishment"
            },
            new()
            {
                Id = 4,
                Type = InventoryTransactionType.Issue,
                WarehouseId = 1,
                ProductId = 1,
                Quantity = 2,
                IssuedByUserId = 1,
                TransactionDate = DateTime.UtcNow.AddDays(-10),
                ReferenceNumber = "SO-2001",
                Notes = "Issued for customer order"
            },
            new()
            {
                Type = InventoryTransactionType.Issue,
                WarehouseId = 1,
                ProductId = 3,
                Quantity = 5,
                IssuedByUserId = 2,
                TransactionDate = DateTime.UtcNow.AddDays(-9),
                ReferenceNumber = "SO-2002",
                Notes = "Issued to branch"
            },
            new()
            {
                Type = InventoryTransactionType.Receipt,
                WarehouseId = 1,
                ProductId = 1,
                Quantity = 1,
                ReceivedByUserId = 1,
                TransactionDate = DateTime.UtcNow.AddDays(-8),
                ReferenceNumber = "RT-3001",
                Notes = "Customer returned item"
            },
            new()
            {
                Type = InventoryTransactionType.Adjustment,
                WarehouseId = 2,
                ProductId = 4,
                Quantity = -2,
                IssuedByUserId = 2,
                TransactionDate = DateTime.UtcNow.AddDays(-7),
                ReferenceNumber = "ADJ-4001",
                Notes = "Damaged items removed"
            },
            new()
            {
                Type = InventoryTransactionType.Adjustment,
                WarehouseId = 3,
                ProductId = 6,
                Quantity = 4,
                ReceivedByUserId = 1,
                TransactionDate = DateTime.UtcNow.AddDays(-6),
                ReferenceNumber = "ADJ-4002",
                Notes = "Inventory count correction"
            },
            new()
            {
                Id = 9,
                Type = InventoryTransactionType.Receipt,
                WarehouseId = 1,
                ProductId = 5,
                Quantity = 3,
                IssuedByUserId = 1,
                ReceivedByUserId = 2,
                TransactionDate = DateTime.UtcNow.AddDays(-5),
                ReferenceNumber = "TR-5001",
                Notes = "Transferred to Alexandria warehouse"
            },
            new()
            {
                Type = InventoryTransactionType.Receipt,
                WarehouseId = 3,
                ProductId = 6,
                Quantity = 12,
                ReceivedByUserId = 2,
                TransactionDate = DateTime.UtcNow.AddDays(-2),
                ReferenceNumber = "PO-1004",
                Notes = "Restocked from supplier"
            }
        };

        await _dbContext.InventoryTransactions.AddRangeAsync(transactions);
        await _dbContext.SaveChangesAsync();
    }
}