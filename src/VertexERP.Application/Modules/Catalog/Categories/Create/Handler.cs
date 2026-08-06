using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
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
        string? imageUrl = null;

        if (request.Image is not null)
        {
            imageUrl = await fileStorage.UploadAsync(request.Image, "categories", ct);
        }

        var category = new Category(request.Name.ToCleanString(), request.Description, imageUrl);

        dbContext.Categories.Add(category);

        try
        {
            await dbContext.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
        {
            if (imageUrl is not null)
                await fileStorage.DeleteAsync(imageUrl, ct);

            return Result<Response>.Conflict("Category name already exists.");
        }

        return Result<Response>.Created(category.Adapt<Response>());
    }
}