
namespace VertexERP.Application.Modules.Inventory.StockMovements.Receive;

public sealed record Request(
    Guid ProductId,
    Guid WarehouseId,
    decimal Quantity,
    string? ReferenceNumber,
    string? Description);