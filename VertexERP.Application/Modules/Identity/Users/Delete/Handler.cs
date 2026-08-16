using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Delete;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid id, CancellationToken ct)
    {
        var deleted = await dbContext.Users
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(ct);

        if (deleted == 0)
            return Result<Response>.NotFound("User not found.");

        return Result<Response>.Success( new Response { Id = id }, "User deleted successfully.");
    }
}