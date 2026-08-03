using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;

namespace VertexERP.Infrastructure.Services.Identity;

public sealed class UserLookupService(
    IAppDbContext dbContext)
    : IUserLookupService
{
    public async Task<IReadOnlyList<Guid>> GetUserIdsByPermissionAsync(
    string permission,
    CancellationToken ct = default)
    {
        return await dbContext.RolePermissions
            .AsNoTracking()
            .Where(rp => rp.Permission == permission)
            .SelectMany(rp => rp.Role.UserRoles)
            .Select(ur => ur.UserId)
            .ToListAsync(ct);
    }
}