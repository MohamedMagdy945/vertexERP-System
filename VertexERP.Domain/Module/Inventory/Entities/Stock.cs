using VertexERP.Domain.Common;
using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Domain.Module.Inventory.Entities;

public sealed class Stock : AuditableEntity
{
    public Guid ProductId { get; private set; }
    public Product Product { get; set; } = default!;
    public Guid WarehouseId { get; private set; }
    public Warehouse Warehouse { get; set; } = default!;

    public decimal Quantity { get; private set; }

    private Stock() { }

    private Stock(Guid productId,Guid warehouseId)
    {
        ProductId = productId;
        WarehouseId = warehouseId;
    }

    public static Stock Create(Guid productId,Guid warehouseId)
    {
        return new Stock(productId, warehouseId);
    }

    public Result Receive(decimal quantity)
    {
        if (quantity <= 0)
            return Result.Failure("Receive quantity must be greater than zero.");

        Quantity += quantity;

        MarkAsUpdated();
        return Result.Success();
    }

    public Result Issue(decimal quantity)
    {
        if (quantity <= 0)
            return Result.Failure("Issue quantity must be greater than zero.");

        if (Quantity < quantity)
            return Result.Failure("Insufficient stock.");

        Quantity -= quantity;
        MarkAsUpdated();
   
        return Result.Success();
    } 

}