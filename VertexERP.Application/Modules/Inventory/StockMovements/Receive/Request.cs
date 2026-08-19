using VertexERP.Application.Shared.Pagination;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Application.Modules.Inventory.StockMovements.Receive;

public sealed record Request(
    Guid ProductId,
    Guid WarehouseId,
    decimal Quantity,
    string? Description);