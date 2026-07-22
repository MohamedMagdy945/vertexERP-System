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
            var term = $"%{request.SearchTerm.Trim()}%";

            query = query.Where(x => EF.Functions.Like(x.FullName, term) ||
                     EF.Functions.Like(x.Email, term));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query.ProjectToType<Response>().ApplyPagination(request.PageNumber, request.PageSize)
                    .ToListAsync(cancellationToken);

        return Result<Page<Response>>.Success(Page<Response>.Create(items, totalCount, request.PageNumber, request.PageSize));
    }
}