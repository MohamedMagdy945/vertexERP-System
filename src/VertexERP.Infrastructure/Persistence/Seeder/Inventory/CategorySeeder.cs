using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Infrastructure.Persistence.Seeder.Inventory;

public class CategorySeeder
{
    private readonly IApplicationDbContext _dbContext;

    public CategorySeeder(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (await _dbContext.Categories.AnyAsync())
            return;

        var categories = new List<Category>
        {
            new()
            {
                Name = "Electronics"
            },
            new()
            {
                Name = "Accessories"
            },
            new()
            {
                Name = "Furniture"
            }
        };

        await _dbContext.Categories.AddRangeAsync(categories);
        await _dbContext.SaveChangesAsync();
    }
}