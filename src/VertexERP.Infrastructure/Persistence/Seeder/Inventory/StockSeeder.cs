using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Infrastructure.Persistence.Seeder.Inventory;

public class StockSeeder
{
    private readonly IApplicationDbContext _dbContext;

    public StockSeeder(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (await _dbContext.Stocks.AnyAsync())
            return;

        var stocks = new List<Stock>
        {
            new()
            {
                ProductId = 1,
                WarehouseId = 1,
                Quantity = 15,
                LastUpdated = DateTime.UtcNow
            },
            new()
            {
                ProductId = 2,
                WarehouseId = 1,
                Quantity = 8,
                LastUpdated = DateTime.UtcNow
            },
            new()
            {
                Id = 3,
                ProductId = 3,
                WarehouseId = 1,
                Quantity = 40,
                LastUpdated = DateTime.UtcNow
            },
            new()
            {
                ProductId = 4,
                WarehouseId = 2,
                Quantity = 25,
                LastUpdated = DateTime.UtcNow
            },
            new()
            {
                ProductId = 5,
                WarehouseId = 2,
                Quantity = 10,
                LastUpdated = DateTime.UtcNow
            },
            new()
            {
                ProductId = 5,
                WarehouseId = 3,
                Quantity = 12,
                LastUpdated = DateTime.UtcNow
            },
            new()
            {
                ProductId = 1,
                WarehouseId = 2,
                Quantity = 5,
                LastUpdated = DateTime.UtcNow
            },
            new()
            {
                ProductId = 3,
                WarehouseId = 3,
                Quantity = 18,
                LastUpdated = DateTime.UtcNow
            }
        };

        await _dbContext.Stocks.AddRangeAsync(stocks);
        await _dbContext.SaveChangesAsync();
    }
}