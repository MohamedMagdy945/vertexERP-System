using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Cache;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;

namespace VertexERP.Infrastructure.Services.Identity.UserPermission;

public sealed class UserPermissionService(IApplicationDbContext dbContext, IUserPermissionCache permissionCache)
    : IUserPermissionService

{
    public async Task<HashSet<string>> GetPermissionsAsync(Guid userId, CancellationToken ct = default)
    {
        var cachedPermissions = await permissionCache.GetAsync(userId, ct);

        if (cachedPermissions is not null)
            return cachedPermissions;


        var permissions = await dbContext.UserRoles.AsNoTracking()
                         .Where(ur => ur.UserId == userId).SelectMany(ur => ur.Role.RolePermissions)
                         .Select(rp => rp.Permission.Name).ToListAsync(ct);

        var result = permissions.ToHashSet();

        await permissionCache.SetAsync(userId, result, ct);

        return result;
    }

}