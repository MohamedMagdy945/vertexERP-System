using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Catalog.Entities;

public sealed class Product : Entity
{
    // Basic Information
    public string Name { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public string? Barcode { get; private set; } = default!;
    public string? Description { get; private set; } = default!;

    // Pricing
    public decimal CostPrice { get; private set; }
    public decimal SellingPrice { get; private set; }

    // Relationships
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = default!;

    public Guid UnitId { get; private set; }
    public MeasurementUnit Unit { get; private set; } = default!;

    // Status
    public bool IsAvailable { get; private set; }

    // Navigation Collections
    public ICollection<ProductImage> Images { get; } = [];

    private Product() { }

    public Product(string name, string code, decimal costPrice, decimal sellingPrice
                    , Guid categoryId, Guid unitId, string? barcode = null, string? description = null)
    {
        Name = name;
        Code = code;
        Barcode = barcode;
        Description = description;

        CostPrice = costPrice;
        SellingPrice = sellingPrice;
        CategoryId = categoryId;

        UnitId = unitId;
        IsAvailable = true;
    }

    public void Update(string name, string code, decimal costPrice, decimal sellingPrice,
            Guid categoryId, Guid unitId, string? description)
    {
        Name = name;
        Code = code;
        Description = description;

        CostPrice = costPrice;
        SellingPrice = sellingPrice;

        CategoryId = categoryId;
        UnitId = unitId;

        MarkAsUpdated();
    }

    public void Activate()
    {
        IsAvailable = true;
        MarkAsUpdated();
    }

    public void Deactivate()
    {
        IsAvailable = false;
        MarkAsUpdated();
    }
    public void AddImages(IEnumerable<string> imagePaths)
    {
        foreach (var path in imagePaths)
        {
            if (string.IsNullOrWhiteSpace(path)) continue;

            Images.Add(new ProductImage(path));
        }
    }
}