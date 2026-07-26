using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.Roles.Update;

public sealed class Handler(IApplicationDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
           .Include(x => x.UserRoles)
           .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user is null)
        {
            return Result<Response>.NotFound("User not found.");
        }

        var requestedRoleIds = request.RoleIds.ToHashSet();

        var existingRoleIds = user.UserRoles
            .Select(x => x.RoleId)
            .ToHashSet();

        var rolesToRemove = user.UserRoles
            .Where(x => !requestedRoleIds.Contains(x.RoleId))
            .ToList();

        var newRoleIdsToAdd = requestedRoleIds
            .Except(existingRoleIds)
            .ToList();

        foreach (var role in rolesToRemove)
        {
            user.UserRoles.Remove(role);
        }

        foreach (var roleId in newRoleIdsToAdd)
        {
            user.UserRoles.Add(new UserRole(user.Id, roleId));
        }


        await dbContext.SaveChangesAsync(cancellationToken);

        var response = await dbContext.Users
            .Where(x => x.Id == user.Id)
            .AsNoTracking()
            .ToResponse()
            .SingleAsync(cancellationToken);

        return Result<Response>.Success(response);
    }
}