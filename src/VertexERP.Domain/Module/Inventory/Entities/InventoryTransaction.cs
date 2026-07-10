using VertexERP.Domain.Common;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Domain.Module.Inventory.Entities;

public class InventoryTransaction : BaseEntity
{
    public InventoryTransactionType Type { get; set; }

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }

    public int? IssuedByUserId { get; set; }
    public User? IssuedByUser { get; set; }

    public int? ReceivedByUserId { get; set; }
    public User? ReceivedByUser { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? Notes { get; set; }
}

