namespace VertexERP.Application.Modules.Inventory.Stocks.Products;

public sealed class Response
{
    public Guid WarehouseId { get; init; }
    public string WarehouseName { get; init; } = default!;
    public decimal Quantity { get; init; }
}