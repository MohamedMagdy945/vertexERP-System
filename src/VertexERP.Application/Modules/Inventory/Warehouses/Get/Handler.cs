using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Pagination;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Services.Get;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Page<Response>>> HandleAsync(Request request, CancellationToken ct)
    {
        var query = dbContext.Categories.AsNoTracking();

        if (request.SearchTerm is not null)
        {
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{request.SearchTerm}%") ||
                (x.Description != null && EF.Functions.Like(x.Description, $"%{request.SearchTerm}%")));
        }

        var totalCount = await query.CountAsync(ct);

        if (totalCount == 0)
        {
            return Result<Page<Response>>.Success(
                Page<Response>.Create([], 0, request.PageNumber, request.PageSize));
        }

        var page = await query
            .OrderBy(x => x.Name)
            .ToResponse()
            .ToPageAsync(request, ct);

        return Result<Page<Response>>.Success(page);
    }
}