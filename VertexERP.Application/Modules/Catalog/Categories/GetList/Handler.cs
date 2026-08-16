using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Pagination;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categories.GetList;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Page<Response>>> HandleAsync(Request request, CancellationToken ct)
    {

        var query = dbContext.Categories.AsNoTracking();

         var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderBy(x => x.Id)
            .AsNoTracking()
            .ToResponse()
            .ToPageAsync(request, ct);

        return Result<Page<Response>>.Success(items);
    }
}