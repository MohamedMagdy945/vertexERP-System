using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Application.Modules.Inventory.StockMovements.Create;

public sealed record Request(
    Guid WarehouseId,
    Guid ProductId,
    int Quantity,
    StockMovementDirection Direction,
    StockMovementType Type,
    DateTime TransactionDate,
    string? ReferenceNumber,
    string? Notes
);