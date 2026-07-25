using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Update;

public sealed class Handler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {

        var user = await dbContext.Users
             .Where(u => u.Id == request.Id)
             .SingleOrDefaultAsync(cancellationToken);


        if (user is null)
            return Result<Response>.NotFound("User not found.");

        user.Update(request.Name, request.PortalType);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(user.Adapt<Response>());
    }
}