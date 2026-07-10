using VertexERP.Domain.Common;
using VertexERP.Domain.Modules.Inventory.Enums;

namespace VertexERP.Domain.Module.Inventory.Entities;

public class StockMovement : BaseEntity
{
    public int StockId { get; set; }
    public Stock Stock { get; set; } = null!;
    public int QuantityChange { get; set; }
    public StockMovementType Type { get; set; }
    public string? ReferenceNo { get; set; }
    public string? Notes { get; set; }
    public string PerformedBy { get; set; } = string.Empty;
    public DateTime MovementDate { get; set; }
}

