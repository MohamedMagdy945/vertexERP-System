using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Pagination;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Get;

public sealed class Handler(IApplicationDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Query request, CancellationToken cancellationToken)
    {
        var query = dbContext.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = $"%{request.SearchTerm.Trim()}%";

            query = query.Where(x => EF.Functions.Like(x.Email, term));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.Id)
            .AsNoTracking()
            .ToResponse()
            .ApplyPagination(request.PageNumber, request.PageSize)
            .ToListAsync(cancellationToken);

        var page = Page<UserResponse>.Create(items, totalCount, request.PageNumber, request.PageSize);

        return Result<Response>.Success(new Response(page));
    }
}