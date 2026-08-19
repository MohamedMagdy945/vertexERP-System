using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Application.Modules.Inventory.StockMovements.Receive;

public sealed record Response(
    Guid ProductId,
    Guid WarehouseId,
    decimal PreviousQuantity,
    decimal ReceivedQuantity,
    decimal CurrentQuantity);