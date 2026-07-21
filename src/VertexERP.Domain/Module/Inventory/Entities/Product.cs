using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Inventory.Entities;

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
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = default!;

    public int UnitId { get; private set; }
    public Unit Unit { get; private set; } = default!;

    // Status
    public bool IsAvailable { get; private set; }

    // Navigation Collections
    public ICollection<Stock> Stocks { get; } = [];
    public ICollection<ProductImage> Images { get; } = [];

    private Product() { }

    public Product(
        string name,
        string code,
        decimal costPrice,
        decimal sellingPrice,
        int categoryId,
        int unitId,
        string? barcode = null,
        string? description = null)
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

    public void Update(
        string name,
        string code,
        decimal costPrice,
        decimal sellingPrice,
        int categoryId,
        int unitId,
        string? barcode,
        string? description)
    {
        Name = name;
        Code = code;
        Barcode = barcode;
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
}