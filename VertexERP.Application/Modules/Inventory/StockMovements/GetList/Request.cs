using VertexERP.Application.Shared.Pagination;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Application.Modules.Inventory.StockMovements.GetList;

public sealed record Request(
    Guid? ProductId,
    Guid? WarehouseId,
    StockMovementType? Type,
    StockMovementDirection? Direction
) : PageRequest;