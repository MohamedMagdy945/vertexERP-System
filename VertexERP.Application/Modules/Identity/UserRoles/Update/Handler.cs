using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Cache;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.UserRoles.Update;

public sealed class Handler(IAppDbContext dbContext, IUserPermissionCache userPermissionCache) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid userId, Request request,
        CancellationToken ct)
    {
        var user = await dbContext.Users
         .Include(x => x.UserRoles)
         .SingleOrDefaultAsync(x => x.Id == userId, ct);

        if (user is null)
            return Result<Response>.NotFound("User not found.");

        var requestedRoleIds = request.RoleIds.ToHashSet();

        var existingRoleIds = user.UserRoles
            .Select(x => x.RoleId)
            .ToHashSet();

        var rolesToRemove = user.UserRoles
            .Where(x => !requestedRoleIds.Contains(x.RoleId))
            .ToList();

        foreach (var userRole in rolesToRemove)
        {
            user.UserRoles.Remove(userRole);
        }

        var rolesToAdd = requestedRoleIds
            .Except(existingRoleIds);

        foreach (var roleId in rolesToAdd)
        {
            user.AssignRole(roleId);
        }

        if (rolesToRemove.Count > 0 || rolesToAdd.Any())
        {
            await dbContext.SaveChangesAsync(ct);
            await userPermissionCache.RemoveAsync(user.Id, ct);
        }

        return Result<Response>.Success(new Response(user.Id, requestedRoleIds.ToList()));
    }
}


