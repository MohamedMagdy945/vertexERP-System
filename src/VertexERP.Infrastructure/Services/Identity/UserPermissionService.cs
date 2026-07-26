using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Cache;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;

namespace VertexERP.Infrastructure.Services.Identity;

public sealed class UserPermissionService(IAppDbContext dbContext, IUserPermissionCache permissionCache)
    : IUserPermissionService

{
    public async Task<HashSet<string>> GetPermissionsAsync(Guid userId, CancellationToken ct = default)
    {
        var cachedPermissions = await permissionCache.GetAsync(userId, ct);

        if (cachedPermissions is not null)
            return cachedPermissions;


        var permissions = await dbContext.UserRoles
            .GetPermissionNames(userId)
            .ToHashSetAsync(ct);

        var result = permissions.ToHashSet();

        await permissionCache.SetAsync(userId, result, ct);

        return result;
    }

}