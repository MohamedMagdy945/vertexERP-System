using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Me;

public sealed class Handler(IAppDbContext dbContext,
    ICurrentUser currentUserService, IUserPermissionService userPermissionService) : IHandler
{
    public async Task<Result<Response>> HandleAsync(CancellationToken ct)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == currentUserService.UserId)
            .ToResponse()
            .SingleOrDefaultAsync(ct);

        if (user is null)
            return Result<Response>.Unauthorized();

        user.Permissions = await userPermissionService
            .GetPermissionsAsync(currentUserService.UserId, ct);

        return Result<Response>.Success(user);
    }
}