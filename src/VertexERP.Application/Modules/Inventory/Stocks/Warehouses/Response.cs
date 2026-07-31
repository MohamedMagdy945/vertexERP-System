namespace VertexERP.Application.Modules.Inventory.Stocks.Warehouses;

public sealed class Response
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = default!;
    public decimal Quantity { get; init; }
}