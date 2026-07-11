using MediatR;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Queries.GetCategories;

public record GetCategoriesQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<PagedResult<GetCategoriesQueryResponse>>>;