using VertexERP.Domain.Common;
using VertexERP.Domain.Module.Catalog.Entities;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Domain.Module.Inventory.Entities;

public sealed class StockMovement : Entity
{
    public Guid WarehouseId { get; private set; }
    public Warehouse Warehouse { get; private set; } = default!;

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;

    public decimal Quantity { get; private set; }

    public Guid PerformedByUserId { get; private set; }
    public User PerformedByUser { get; private set; } = default!;

    public StockMovementType Type { get; private set; }

    public StockMovementDirection Direction { get; private set; }

    public DateTime TransactionDate { get; private set; }

    public string? ReferenceNumber { get; private set; }

    public string? Notes { get; private set; }

    private StockMovement()
    {
    }

    public StockMovement(
        Guid warehouseId,
        Guid productId,
        decimal quantity,
        Guid performedByUserId,
        StockMovementDirection direction,
        StockMovementType type,
        DateTime transactionDate,
        string? referenceNumber = null,
        string? notes = null)
    {
        WarehouseId = warehouseId;
        ProductId = productId;
        Quantity = quantity;
        PerformedByUserId = performedByUserId;
        Direction = direction;
        Type = type;
        TransactionDate = transactionDate;
        ReferenceNumber = referenceNumber;
        Notes = notes;
    }

    public void UpdateNotes(string? notes)
    {
        Notes = notes;

        MarkAsUpdated();
    }
}