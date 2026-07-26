using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;

namespace VertexERP.Application.Common.Extensions;

public static class UserPermissionExtensions
{
    public static IQueryable<string> GetPermissionNames(
       this IAppDbContext dbContext,
       Guid userId)
    {
        return dbContext.UserRoles
            .AsNoTracking()
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission);
    }
}