using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.Storage;
using VertexERP.Domain.Module.Catalog.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Commands.Create;

public sealed class Handler(IApplicationDbContext dbContext, IFileStorage fileStorage, ILogger<Handler> logger)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var exists = await dbContext.Products.AnyAsync(x => x.Code == request.Code ||
            (!string.IsNullOrWhiteSpace(request.Barcode) && x.Barcode == request.Barcode), cancellationToken);

        if (exists)
            return Result<Response>.Conflict("Product code or barcode already exists.");

        var product = request.Adapt<Product>();

        List<string>? imagePaths = null;

        if (request.Images is { Count: > 0 })
        {
            imagePaths = await fileStorage.UploadManyAsync(request.Images, $"products/{request.Code}", cancellationToken);

            product.AddImages(imagePaths);
        }

        dbContext.Products.Add(product);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            if (imagePaths is { Count: > 0 })
            {
                await fileStorage.DeleteManyAsync(imagePaths, cancellationToken);
            }

            throw;
        }
        return Result<Response>.Created(product.Adapt<Response>());
    }
}