using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.Storage;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Catalog.Categories.Update;

public sealed class Handler(IAppDbContext dbContext, IFileStorage fileStorage) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var category = await dbContext.Categories
            .SingleOrDefaultAsync(x => x.Id == request.Id, ct);

        if (category is null)
            return Result<Response>.NotFound("Category not found.");

        var categoryName = Category.FormatName(request.Name);

        var exists = await dbContext.Categories
            .AnyAsync(x => x.Id != request.Id && x.Name == categoryName, ct);

        if (exists)
            return Result<Response>.Conflict("Category name already exists.");

        string? imageUrl = null;

        if (request.Image is not null)
        {
            if (!string.IsNullOrWhiteSpace(category.ImageUrl))
                await fileStorage.DeleteAsync(category.ImageUrl, ct);

            imageUrl = await fileStorage.UploadAsync(request.Image, "categories", ct);
        }
        category.Update(request.Name, request.Description, imageUrl);

        await dbContext.SaveChangesAsync(ct);

        return Result<Response>.Success(category.Adapt<Response>());
    }
}