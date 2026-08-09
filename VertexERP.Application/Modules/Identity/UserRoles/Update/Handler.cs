using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Cache;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.UserRoles.Update;

public sealed class Handler(IAppDbContext dbContext, IUserPermissionCache userPermissionCache) : IHandler
{
    public async Task<Result<Response>> HandleAsync(
    Request request,
    CancellationToken ct)
    {
        var user = await dbContext.Users
            .Include(x => x.UserRoles)
            .SingleOrDefaultAsync(x => x.Id == request.UserId, ct);

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

        var rolesToAdd = requestedRoleIds.Except(existingRoleIds);

        foreach (var roleId in rolesToAdd)
        {
            user.UserRoles.Add(new UserRole(user.Id, roleId));
        }

        await dbContext.SaveChangesAsync(ct);

        await userPermissionCache.RemoveAsync(user.Id, ct);

        var roles = await dbContext.UserRoles
            .AsNoTracking()
            .Where(x => x.UserId == user.Id)
            .Select(x => new RoleResponse
            {
                Id = x.RoleId,
                Name = x.Role.Name
            })
            .ToListAsync(ct);

        return Result<Response>.Success(new Response
        {
            UserId = user.Id,
            Roles = roles
        });
    }
}


