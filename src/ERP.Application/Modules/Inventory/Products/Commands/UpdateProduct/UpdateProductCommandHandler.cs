using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.UpdateProduct;


public class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, Result<UpdateProductCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UpdateProductCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public UpdateProductCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<UpdateProductCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _dbContext = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<UpdateProductCommandResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var result = Result<UpdateProductCommandResponse>.Create();

        var existingProduct = await _dbContext.Products.FindAsync(request.Id, cancellationToken);

        if (existingProduct == null)
            return result.NotFound($"Product with Id {request.Id} not found");

        string? imageUrl = null;

        if (request.Image is not null && request.Image.Length > 0)
        {
            if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
            {
                await _fileStorage.DeleteAsync(existingProduct.ImageUrl, cancellationToken);
            }

            using var stream = request.Image.OpenReadStream();

            imageUrl = await _fileStorage.UploadAsync(stream, request.Image.FileName,
                request.Image.ContentType, "products", cancellationToken);

            if (imageUrl == null)
                return result.Failure("Image upload failed");
        }

        request.Adapt(existingProduct);

        if (imageUrl is not null)
        {
            existingProduct.ImageUrl = imageUrl;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return result.Updated(existingProduct.Adapt<UpdateProductCommandResponse>());
    }
}

