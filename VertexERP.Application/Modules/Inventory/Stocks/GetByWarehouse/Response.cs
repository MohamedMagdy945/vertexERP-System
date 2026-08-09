namespace VertexERP.Application.Modules.Inventory.Stocks.GetByWarehouse;

public sealed class Response
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string ProductCode { get; set; } = default!;
    public decimal Quantity { get; set; }
}