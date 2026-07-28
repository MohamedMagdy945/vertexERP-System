using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.Storage;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Catalog.Categories.Create;

public sealed class Handler(IAppDbContext dbContext, IFileStorage fileStorage) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var categoryName = request.Name.ToCleanString();

        var exists = await dbContext.Categories.AnyAsync(x => x.Name == categoryName, ct);

        if (exists)
            return Result<Response>.Conflict("Category name already exists.");

        string? imageUrl = null;

        if (request.Image is not null)
        {
            imageUrl = await fileStorage.UploadAsync(request.Image, "categories", ct);
        }

        var category = new Category(request.Name, request.Description, imageUrl);

        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync(ct);

        return Result<Response>.Created(category.Adapt<Response>());
    }
}