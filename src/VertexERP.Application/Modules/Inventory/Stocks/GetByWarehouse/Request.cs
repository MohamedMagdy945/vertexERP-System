using VertexERP.Application.Shared.Pagination;

namespace VertexERP.Application.Modules.Inventory.Stocks.GetByWarehouse;

public sealed record Request(
    Guid WarehouseId
) : PageRequest;