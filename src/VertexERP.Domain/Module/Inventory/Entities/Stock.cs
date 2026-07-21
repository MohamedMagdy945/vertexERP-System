using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Inventory.Entities;

public sealed class Stock : AuditableEntity
{
    public int ProductId { get; private set; }

    public Product Product { get; private set; } = default!;

    public int WarehouseId { get; private set; }

    public Warehouse Warehouse { get; private set; } = default!;

    public int Quantity { get; private set; }

    private Stock() { }

    public Stock(int productId, int warehouseId, int quantity = 0)
    {
        ProductId = productId;
        WarehouseId = warehouseId;
        Quantity = quantity;
    }

    public void Increase(int quantity)
    {
        Quantity += quantity;
        MarkAsUpdated();
    }

    public void Decrease(int quantity)
    {
        Quantity -= quantity;
        MarkAsUpdated();
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
        MarkAsUpdated();
    }
}