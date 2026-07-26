using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Roles.Get;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {

        var exists = await dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Id == request.UserId, cancellationToken);

        if (!exists)
            return Result<Response>.NotFound("User not found.");

        var roles = await dbContext.UserRoles
            .AsNoTracking()
            .Where(ur => ur.UserId == request.UserId)
            .Select(ur => new RoleResponse { Id = ur.Role.Id, Name = ur.Role.Name })
            .ToListAsync(cancellationToken);

        return Result<Response>.Success(new Response
        {
            UserId = request.UserId,
            Roles = roles
        });
    }
}