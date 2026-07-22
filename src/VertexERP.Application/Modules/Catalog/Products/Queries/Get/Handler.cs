using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Queries.Get;

public sealed class Handler(IApplicationDbContext dbContext)
    : IRequestHandler<Query, Result<Page<Response>>>
{
    public async ValueTask<Result<Page<Response>>> Handle(Query request, CancellationToken cancellationToken)
    {
        var query = dbContext.Products.AsNoTracking();

        if (request.Code is not null)
        {
            query = query.Where(x => x.Code == request.Code);
        }

        if (request.SearchTerm is not null)
        {
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{request.SearchTerm}%") ||
                (x.Description != null && EF.Functions.Like(x.Description, $"%{request.SearchTerm}%")));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        if (totalCount == 0)
            return Result<Page<Response>>.Success(Page<Response>.Create([], 0, request.PageNumber, request.PageSize));

        var items = await query
            .OrderBy(x => x.Name).Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize).ProjectToType<Response>().ToListAsync(cancellationToken);

        return Result<Page<Response>>.Success(Page<Response>.Create(items, totalCount, request.PageNumber, request.PageSize));
    }
}