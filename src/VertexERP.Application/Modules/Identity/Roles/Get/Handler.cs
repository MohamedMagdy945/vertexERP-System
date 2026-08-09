using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Pagination;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Roles.Get;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Page<Response>>> HandleAsync(Request request, CancellationToken ct)
    {
        var query = dbContext.Roles
            .AsNoTracking()
            .Where(r => r.Name != SecurityRoles.SystemAdmin
             && r.Name != SecurityRoles.SecurityAdmin); ;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = $"%{request.SearchTerm.Trim()}%";

            query = query.Where(x => EF.Functions.Like(x.Name, term));
        }

        var roles = await query
            .OrderBy(r => r.Name)
            .ToResponse()
            .ToPageAsync(request, ct);

        return Result<Page<Response>>.Success(roles);
    }
}