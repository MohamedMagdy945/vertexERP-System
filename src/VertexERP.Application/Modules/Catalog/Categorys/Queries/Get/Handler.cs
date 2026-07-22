using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categorys.Queries.Get;

public sealed class Handler(IApplicationDbContext dbContext, ILogger<Handler> logger)
    : IRequestHandler<Query, Result<Page<Response>>>
{
    public async ValueTask<Result<Page<Response>>> Handle(Query request, CancellationToken cancellationToken)
    {
        var query = dbContext.Categories.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = $"%{request.SearchTerm.Trim()}%";

            query = query.Where(x => EF.Functions.Like(x.Name, term) ||
                    (x.Description != null && EF.Functions.Like(x.Description, term)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        if (totalCount == 0)
            return Result<Page<Response>>.Success(Page<Response>.Create([], 0, request.PageNumber, request.PageSize));

        var items = await query.OrderBy(x => x.Name).Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize).ProjectToType<Response>().ToListAsync(cancellationToken);

        return Result<Page<Response>>.Success(
            Page<Response>.Create(items, totalCount, request.PageNumber, request.PageSize));
    }
}