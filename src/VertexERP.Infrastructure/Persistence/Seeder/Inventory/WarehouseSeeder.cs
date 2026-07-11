using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Infrastructure.Persistence.Seeder.Inventory;

public class WarehouseSeeder
{
    private readonly IApplicationDbContext _dbContext;

    public WarehouseSeeder(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (await _dbContext.Warehouses.AnyAsync())
            return;

        var warehouses = new List<Warehouse>
        {
            new()
            {
                Name = "Main Warehouse",
                Code = "WH001",
                Location = "Cairo"
            },
            new()
            {
                Name = "Alexandria Warehouse",
                Code = "WH002",
                Location = "Alexandria"
            },
            new()
            {
                Name = "Giza Warehouse",
                Code = "WH003",
                Location = "Giza"
            }
        };

        await _dbContext.Warehouses.AddRangeAsync(warehouses);
        await _dbContext.SaveChangesAsync();
    }
}
