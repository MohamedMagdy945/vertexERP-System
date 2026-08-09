using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.Storage;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Images.Delete;

public sealed class Handler(IAppDbContext dbContext, IFileStorage fileStorage) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var image = await dbContext.ProductImages
         .Where(x => x.ProductId == request.ProductId && x.Id == request.ImageId)
         .Select(x => new { x.Url })
         .FirstOrDefaultAsync(ct);

        if (image is null)
            return Result<Response>.NotFound("Product image not found.");

        await dbContext.ProductImages
            .Where(x => x.Id == request.ImageId)
            .ExecuteDeleteAsync(ct);


        await fileStorage.DeleteAsync(image.Url, ct);

        return Result<Response>.Success(new Response()
        {
            ProductId = request.ProductId,
            ImageId = request.ImageId
        }, "Product image deleted successfully.");
    }
}