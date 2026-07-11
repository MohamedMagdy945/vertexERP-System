using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Infrastructure.Persistence.Seeder.Inventory;

public class ProductSeeder
{
    private readonly IApplicationDbContext _dbcontext;

    public ProductSeeder(
        IApplicationDbContext dbcontext
     )
    {
        _dbcontext = dbcontext;
    }

    public async Task SeedAsync()
    {
        if (await _dbcontext.Products.AnyAsync())
            return;

        var products = new List<Product>
        {
            new Product
            {
                Name = "Laptop Dell Inspiron 15",
                ImageUrl = "images/products/dell-inspiron-15.jpg",
                Barcode = "100000000001",
                Description = "Dell Inspiron 15 Laptop",
                Code = "PRD-001",
                CostPrice = 25000m,
                CategoryId = 1,
                Unit = UnitType.Piece,
                IsAvailable = true
            },
            new Product
            {
                Name = "HP Laser Printer",
                ImageUrl = "images/products/hp-printer.jpg",
                Barcode = "100000000002",
                Description = "HP LaserJet Printer",
                Code = "PRD-002",
                CostPrice = 8500m,
                CategoryId = 1,
                Unit = UnitType.Piece,
                IsAvailable = true
            },
            new Product
            {
                Name = "Wireless Mouse",
                ImageUrl = "images/products/wireless-mouse.jpg",
                Barcode = "100000000003",
                Description = "2.4G Wireless Mouse",
                Code = "PRD-003",
                CostPrice = 250m,
                CategoryId = 2,
                Unit = UnitType.Piece,
                IsAvailable = true
            },
            new Product
            {
                Name = "Mechanical Keyboard",
                ImageUrl = "images/products/mechanical-keyboard.jpg",
                Barcode = "100000000004",
                Description = "RGB Mechanical Keyboard",
                Code = "PRD-004",
                CostPrice = 900m,
                CategoryId = 2,
                Unit = UnitType.Piece,
                IsAvailable = true
            },
            new Product
            {
                Name = "Office Chair",
                ImageUrl = "images/products/office-chair.jpg",
                Barcode = "100000000005",
                Description = "Ergonomic Office Chair",
                Code = "PRD-005",
                CostPrice = 3200m,
                CategoryId = 3,
                Unit = UnitType.Piece,
                IsAvailable = true
            }
        };
        await _dbcontext.Products.AddRangeAsync(products);
        await _dbcontext.SaveChangesAsync();
    }
}


