using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.Storage;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categories.Update;

public sealed class Handler(IAppDbContext dbContext, IFileStorage fileStorage) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var category = await dbContext.Categories
            .SingleOrDefaultAsync(x => x.Id == request.Id, ct);

        if (category is null)
            return Result<Response>.NotFound("Category not found.");

        var categoryName = request.Name.ToCleanString();

        var exists = await dbContext.Categories
            .AnyAsync(x => x.Id != request.Id && x.Name == categoryName, ct);

        if (exists)
            return Result<Response>.Conflict("Category name already exists.");

        string? newImageUrl = category.ImageUrl;
        string? oldImageUrlToDelete = null;

        if (request.Image is not null)
        {
            newImageUrl = await fileStorage.UploadAsync(request.Image, "categories", ct);

            if (!string.IsNullOrWhiteSpace(category.ImageUrl))
            {
                oldImageUrlToDelete = category.ImageUrl;
            }
        }

        await dbContext.SaveChangesAsync(ct);

        if (oldImageUrlToDelete is not null)
        {
            await fileStorage.DeleteAsync(oldImageUrlToDelete, ct);
        }

        return Result<Response>.Success(category.Adapt<Response>());
    }
}