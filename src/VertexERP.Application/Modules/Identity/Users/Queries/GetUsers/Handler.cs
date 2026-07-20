using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Queries.GetUsers;

public sealed class Handler(
    IApplicationDbContext dbContext, ILogger<Handler> logger)
    : IRequestHandler<Query, Result<Page<Response>>>
{
    public async ValueTask<Result<Page<Response>>> Handle(Query request, CancellationToken cancellationToken)
    {
        var query = dbContext.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();

            query = query.Where(u =>
                u.FullName.ToLower().Contains(searchTerm) ||
                u.Email.ToLower().Contains(searchTerm));
        }

        var projectedQuery = query.ProjectToType<Response>();

        var totalCount = await projectedQuery.CountAsync(cancellationToken);

        var items = await projectedQuery.ApplyPagination(request.PageNumber, request.PageSize)
                                .ToListAsync(cancellationToken);

        var paginatedUsers = Page<Response>.Create(items, totalCount, request.PageNumber, request.PageSize);

        return Result<Page<Response>>.Success(paginatedUsers);
    }
}