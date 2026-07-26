using Microsoft.EntityFrameworkCore;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Common.Extensions;

public static class UserPermissionExtensions
{
    public static IQueryable<string> GetPermissionNames(this IQueryable<UserRole> userRoles, Guid userId)
    {
        return userRoles
            .AsNoTracking()
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission);
    }
}