using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Application.Modules.Inventory.StockAdjustments.Apply;

public sealed record Response(
    Guid Id,
    Guid WarehouseId,
    Guid ProductId,
    decimal AdjustmentQuantity,
    decimal NewStockQuantity,
    StockAdjustmentStatus Status);