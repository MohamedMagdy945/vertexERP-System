using Mapster;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.Storage;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Catalog.Products.Create;

public sealed class Handler(
    IAppDbContext dbContext,
    IFileStorage fileStorage) : IHandler
{
    public async Task<Result<Response>> Handle(Request request, CancellationToken ct)
    {
        var code = request.Code.ToCleanString();
        var barcode = request.Barcode?.ToCleanString();

        var product = new Product(
            request.Name,
            code,
            request.CostPrice,
            request.SellingPrice,
            request.CategoryId,
            request.UnitId,
            barcode,
            request.Description
        );

        List<string>? imagePaths = null;

        try
        {
            if (request.Images is { Count: > 0 })
            {
                imagePaths = await fileStorage.UploadManyAsync(request.Images, $"products/{code}", ct);
                product.AddImages(imagePaths);
            }

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(ct);

            return Result<Response>.Created(product.Adapt<Response>());
        }
        catch
        {
            if (imagePaths is { Count: > 0 })
            {
                try { await fileStorage.DeleteManyAsync(imagePaths, ct); } catch { }
            }

            throw;
        }
    }
}
