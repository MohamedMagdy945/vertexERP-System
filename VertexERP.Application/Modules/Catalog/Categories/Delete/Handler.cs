using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categories.Delete;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid id, CancellationToken ct)
    {
        var affected = await dbContext.Categories
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(ct);

        if (affected == 0)
            return Result<Response>.NotFound("Category not found.");

        return Result<Response>.Success(new Response(id));
    }
}