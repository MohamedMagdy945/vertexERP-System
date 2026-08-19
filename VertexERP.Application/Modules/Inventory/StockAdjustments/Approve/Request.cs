
namespace VertexERP.Application.Modules.Inventory.StockAdjustments.Approve;

public sealed record Request(
    Guid WarehouseId,
    Guid ProductId,
    decimal Quantity,
    string Reason);