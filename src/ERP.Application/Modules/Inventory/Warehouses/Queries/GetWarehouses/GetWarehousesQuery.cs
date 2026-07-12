using MediatR;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Queries.GetWarehouses;

public record GetWarehousesQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<PagedResult<GetWarehousesQueryResponse>>>;