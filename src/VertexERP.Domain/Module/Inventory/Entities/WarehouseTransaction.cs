using VertexERP.Domain.Common;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Domain.Module.Inventory.Entities;

public sealed class WarehouseTransaction : Entity
{
    public Guid WarehouseId { get; private set; }
    public Warehouse Warehouse { get; private set; } = default!;

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;

    public int Quantity { get; private set; }

    public Guid PerformedByUserId { get; private set; }
    public User PerformedByUser { get; private set; } = default!;

    public WarehouseTransactionType Type { get; private set; }

    public TransactionReferenceType ReferenceType { get; private set; }

    public DateTime TransactionDate { get; private set; }

    public string? ReferenceNumber { get; private set; }

    public string? Notes { get; private set; }

    private WarehouseTransaction() { }

    public WarehouseTransaction(
        Guid warehouseId,
        Guid productId,
        int quantity,
        Guid performedByUserId,
        WarehouseTransactionType type,
        TransactionReferenceType referenceType,
        DateTime transactionDate,
        string? referenceNumber = null,
        string? notes = null)
    {
        WarehouseId = warehouseId;
        ProductId = productId;
        Quantity = quantity;
        PerformedByUserId = performedByUserId;
        Type = type;
        ReferenceType = referenceType;
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