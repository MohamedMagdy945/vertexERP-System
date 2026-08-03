using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Delete;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var user = await dbContext.Users
            .SingleOrDefaultAsync(x => x.Id == request.Id, ct);

        if (user is null)
            return Result<Response>.NotFound("User not found.");


        dbContext.Users.Remove(user);

        await dbContext.SaveChangesAsync(ct);

        return Result<Response>.Success(new Response { Id = user.Id }, "User deleted successfully.");
    }
}