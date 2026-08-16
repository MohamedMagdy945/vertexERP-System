using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.Storage;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Delete;

public sealed class Handler(IAppDbContext dbContext, IFileStorage fileStorage) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid id, CancellationToken ct)
    {
        var imagePaths = await dbContext.ProductImages
          .Where(x => x.ProductId == id)
          .Select(x => x.Url)
          .ToListAsync(ct);

        var affectedRows = await dbContext.Products
          .Where(x => x.Id == id)
          .ExecuteDeleteAsync(ct);

        if (affectedRows == 0)
            return Result<Response>.NotFound("Product not found.");

        if (imagePaths is { Count: > 0 })
        {
            await fileStorage.DeleteManyAsync(imagePaths, ct);
        }

        return Result<Response>.Success(new Response(id), "Product deleted successfully.");
    }
}