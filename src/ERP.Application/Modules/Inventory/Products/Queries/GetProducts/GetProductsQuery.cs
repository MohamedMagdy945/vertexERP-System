using MediatR;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Queries.GetProducts;

public record GetProductsQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<PagedResult<GetProductsQueryResponse>>>;