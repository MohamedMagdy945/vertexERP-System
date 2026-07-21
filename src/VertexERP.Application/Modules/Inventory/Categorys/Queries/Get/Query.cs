using Mediator;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Categorys.Queries.Get;

public sealed record Query(int PageNumber = 1, int PageSize = 10, string? SearchTerm = null)
    : IRequest<Result<Page<Response>>>;