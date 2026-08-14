using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Cache;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Roles.Update;

public sealed class Handler(IAppDbContext dbContext, IUserPermissionCache userPermissionCache) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid id, Request request,
        CancellationToken ct)
    {
        var role = await dbContext.Roles
           .Include(x => x.RolePermissions)
           .SingleOrDefaultAsync(x => x.Id == id, ct);

        if (role is null)
            return Result<Response>.NotFound("Role not found.");

        var requestedPermissions = request.Permissions.ToHashSet();

        var existingPermissions = role.RolePermissions
            .Select(x => x.Permission)
            .ToHashSet();

        var permissionsToRemove = role.RolePermissions
            .Where(x => !requestedPermissions.Contains(x.Permission))
            .ToList();

        foreach (var permission in permissionsToRemove)
        {
            role.RolePermissions.Remove(permission);
        }

        var permissionsToAdd = requestedPermissions.Except(existingPermissions);

        foreach (var permission in permissionsToAdd)
        {
            role.RolePermissions.Add(new RolePermission(role.Id, permission));
        }

        await dbContext.SaveChangesAsync(ct);

        var userIds = await dbContext.UserRoles
            .AsNoTracking()
            .Where(x => x.RoleId == role.Id)
            .Select(x => x.UserId)
            .Distinct()
            .ToListAsync(ct);

        foreach (var userId in userIds)
        {
            await userPermissionCache.RemoveAsync(userId, ct);
        }

        return Result<Response>.Success(new Response
        {
            RoleId = role.Id,
            Name = role.Name,
            Permissions = requestedPermissions
        });
    }
}