using Mapster;
using Microsoft.EntityFrameworkCore;
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
    private static class DbConstraints
    {
        public const string UniqueCode = "IX_Products_Code";
        public const string UniqueBarcode = "IX_Products_Barcode";
        public const string FkCategory = "FK_Products_Categories";
        public const string FkUnit = "FK_Products_MeasurementUnits";
    }

    public async Task<Result<Response>> Handle(Request request, CancellationToken ct)
    {
        var code = request.Code.ToCleanString();
        var barcode = request.Barcode?.ToCleanString();

        var product = new Product(request.Name, code, request.CostPrice, request.SellingPrice, request.CategoryId, request.UnitId, barcode, request.Description);

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
        catch (DbUpdateException ex)
        {
            await DeleteImagesAsync();

            var message = ex.InnerException?.Message ?? string.Empty;

            if (message.Contains(DbConstraints.UniqueCode))
                return Result<Response>.Conflict("Product code already exists.");

            if (message.Contains(DbConstraints.UniqueBarcode))
                return Result<Response>.Conflict("Product barcode already exists.");

            if (message.Contains(DbConstraints.FkCategory))
                return Result<Response>.NotFound("Category not found.");

            if (message.Contains(DbConstraints.FkUnit))
                return Result<Response>.NotFound("Measurement unit not found.");

            throw;
        }
        catch
        {
            await DeleteImagesAsync();
            throw;
        }

        async Task DeleteImagesAsync()
        {
            if (imagePaths is not { Count: > 0 })
                return;

            try
            {
                await fileStorage.DeleteManyAsync(imagePaths, ct);
            }
            catch
            {
                // Ignore cleanup failures.
            }
        }
    }
}
