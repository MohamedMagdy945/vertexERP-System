namespace VertexERP.Application.Modules.Inventory.Stocks.GetByProduct;

public sealed class Response
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string ProductCode { get; set; } = default!;
    public decimal TotalQuantity { get; set; }
    public IReadOnlyCollection<WarehouseResponse> Warehouses { get; set; } = [];
}

public sealed class WarehouseResponse
{
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = default!;
    public decimal Quantity { get; set; }
}