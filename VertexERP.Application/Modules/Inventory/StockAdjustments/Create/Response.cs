using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Application.Modules.Inventory.StockAdjustments.Create;
public sealed record Response(
    Guid Id,
    Guid WarehouseId,
    Guid ProductId,
    decimal Quantity,
    string Reason,
    StockAdjustmentStatus Status);