using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Authorization;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Roles.GetById;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request,
     CancellationToken ct)
    {
        var query = dbContext.Roles.AsNoTracking()
          .Where(x => x.Id == request.Id && x.Name != SecurityRoles.SystemAdmin && x.Name != SecurityRoles.SecurityAdmin);

        var role = await query.Select(x => new Response
        {
            Id = x.Id,
            Name = x.Name!,
            Permissions = x.RolePermissions.Select(rp => rp.Permission).ToHashSet()
        }).SingleOrDefaultAsync(ct);

        if (role is null)
            return Result<Response>.NotFound("Role not found.");

        return Result<Response>.Success(role);
    }
}