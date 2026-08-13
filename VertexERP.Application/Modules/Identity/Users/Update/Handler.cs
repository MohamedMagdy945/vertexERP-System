using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.Update;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid id, Request request, CancellationToken ct)
    {
        var user = await dbContext.Users
            .Include(x => x.UserRoles)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        if (user is null)
            return Result<Response>.NotFound("User not found.");

        request.Adapt(user);

        user.ClearUserRoles();

        foreach (var roleId in request.RoleIds)
        {
            user.AssignRole(roleId);
        }

        await dbContext.SaveChangesAsync(ct);

        return Result<Response>.Success(user.Adapt<Response>(),"User updated successfully.");
    }
}