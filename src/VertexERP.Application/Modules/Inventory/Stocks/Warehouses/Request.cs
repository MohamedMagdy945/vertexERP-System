using VertexERP.Application.Shared.Pagination;

namespace VertexERP.Application.Modules.Inventory.Stocks.Warehouses;

public sealed record Request(
    Guid WarehouseId,
    PageRequest PageRequest);
