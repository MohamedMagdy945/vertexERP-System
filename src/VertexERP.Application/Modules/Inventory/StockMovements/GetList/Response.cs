using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Application.Modules.Inventory.StockMovements.GetList;

public sealed class Response
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid WarehouseId { get; init; }
    public int Quantity { get; init; }
    public StockMovementDirection Direction { get; init; }
    public StockMovementType Type { get; init; }
    public DateTime TransactionDate { get; init; }
    public string? ReferenceNumber { get; init; }
}