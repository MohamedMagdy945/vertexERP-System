using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Infrastructure.Persistence.Seeder.Inventory;

public class UnitSeeder
{
    private readonly IApplicationDbContext _dbContext;

    public UnitSeeder(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (await _dbContext.Units.AnyAsync())
            return;

        var units = new List<Unit>
        {
            new()
            {
                Name = "Piece",
                Symbol = "Pc"
            },
            new()
            {
                Name = "Box",
                Symbol = "Box"
            },
            new()
            {
                Name = "Kilogram",
                Symbol = "Kg"
            }
        };

        await _dbContext.Units.AddRangeAsync(units);
        await _dbContext.SaveChangesAsync();
    }
}