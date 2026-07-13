namespace VertexERP.Application.Modules.Inventory.Stocks.Commands.CreateStock;

public record CreateStockCommandResponse
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
}