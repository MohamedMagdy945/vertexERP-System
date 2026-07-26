using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Roles.Get;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var query = dbContext.Roles.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = $"%{request.SearchTerm.Trim()}%";

            query = query.Where(x => EF.Functions.Like(x.Name, term));
        }

        var roles = await query
            .OrderBy(r => r.Name)
            .ToResponse()
            .ToPageAsync(request, ct);

        return Result<Response>.Success(new Response { Roles = roles });
    }
}