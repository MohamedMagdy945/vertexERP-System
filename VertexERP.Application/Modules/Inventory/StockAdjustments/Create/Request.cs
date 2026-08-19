namespace VertexERP.Application.Modules.Inventory.StockAdjustments.Create;
public sealed record Request(
    Guid WarehouseId,
    Guid ProductId,
    decimal Quantity,
    string Reason);