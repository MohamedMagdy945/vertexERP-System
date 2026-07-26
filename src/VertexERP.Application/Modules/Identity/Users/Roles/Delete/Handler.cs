using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Cache;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Roles.Delete;

public sealed class Handler(IAppDbContext dbContext, IUserPermissionCache userPermissionCache) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var userRole = await dbContext.UserRoles.FirstOrDefaultAsync(
        x => x.UserId == request.UserId &&
             x.RoleId == request.RoleId,
        ct);

        if (userRole is null)
        {
            return Result<Response>.NotFound("Role assignment not found.");
        }

        dbContext.UserRoles.Remove(userRole);

        await dbContext.SaveChangesAsync(ct);

        var permissions = await dbContext.UserRoles
            .GetPermissionNames(request.UserId)
            .ToHashSetAsync(ct);

        await userPermissionCache.SetAsync(request.UserId, permissions, ct);

        return Result<Response>.Success(new Response());
    }
}