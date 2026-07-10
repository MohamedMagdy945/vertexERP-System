using MediatR;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Queries.GetUsersQuery;

public record GetUsersQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<PagedResult<GetUsersQueryResponse>>>;