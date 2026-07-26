using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;

namespace VertexERP.Application.Common.Extensions;

public static class UserRoleQueryExtensions
{
    public static IQueryable<string> GetRoleNames(this IAppDbContext dbContext, Guid userId)
    {
        return dbContext.UserRoles
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(x => x.Role.Name);
    }
}