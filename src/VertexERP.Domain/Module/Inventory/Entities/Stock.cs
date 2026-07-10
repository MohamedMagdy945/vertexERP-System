using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Inventory.Entities;

public class Stock : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;

    public int Quantity { get; set; } = default!;

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public ICollection<StockMovement> Movements { get; set; } = new List<StockMovement>();
}