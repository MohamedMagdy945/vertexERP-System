using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.Storage;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Catalog.Products.Images.Upload;

public sealed class Handler(
    IAppDbContext dbContext,
    IFileStorage fileStorage) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid productId ,Request request, CancellationToken ct)
    {
        var uploadedImagesCount = request.Images?.Count ?? 0;

        if (uploadedImagesCount == 0)
            return Result<Response>.Failure("No images provided.");

        var context = await dbContext.Products
            .Where(x => x.Id == productId)
            .ToContext()
            .FirstOrDefaultAsync(ct);

        if (context is null)
            return Result<Response>.NotFound("Product not found.");

        var remainingAllowed = 6 - context.CurrentImagesCount;

        if (uploadedImagesCount > remainingAllowed)
        {
            return Result<Response>.Failure(
                remainingAllowed > 0
                    ? $"Product currently has {context.CurrentImagesCount} images. You can only add up to {remainingAllowed} more."
                    : "Product already has the maximum limit of 6 images.");
        }

        var folderPath = $"products/{context.Code}";

        var urls = await fileStorage.UploadManyAsync(request.Images!, folderPath, ct);

        try
        {
            var images = urls
                .Select(url => new ProductImage(url, productId))
                .ToList();

            dbContext.ProductImages.AddRange(images);

            await dbContext.SaveChangesAsync(ct);

            var respone = new Response(productId, images.Select(x => new ImageResponse(x.Id, x.Url)).ToList());
            return Result<Response>.Success(respone, "Images uploaded successfully.");
        }
        catch
        {
            await fileStorage.DeleteManyAsync(urls, ct);
            throw;
        }
    }
}