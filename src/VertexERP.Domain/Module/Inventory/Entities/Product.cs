using VertexERP.Domain.Common;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Domain.Module.Inventory.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal CostPrice { get; set; }
    public int CategoryId { get; set; }
    public Unit Unit { get; set; }
    public Category Category { get; set; } = default!;
    public bool IsAvailable { get; set; } = true;
    public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

}

