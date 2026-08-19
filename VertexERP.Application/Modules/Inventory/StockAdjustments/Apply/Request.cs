namespace VertexERP.Application.Modules.Inventory.StockAdjustments.Apply;

public sealed record Request(
    Guid WarehouseId,
    Guid ProductId,
    decimal Quantity,
    string Reason);