using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.CreateProduct;


public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, Result<CreateProductCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public CreateProductCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<CreateProductCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _dbContext = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<CreateProductCommandResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = Result<CreateProductCommandResponse>.Create();

        string? imageUrl = null;
        if (request.Image is not null && request.Image.Length > 0)
        {
            using var stream = request.Image.OpenReadStream();

            imageUrl = await _fileStorage.UploadAsync(stream, request.Image.FileName,
                request.Image.ContentType, "products", cancellationToken);

            if (imageUrl == null)
                return result.Failure("Image upload failed");
        }

        var product = request.Adapt<Product>();
        product.ImageUrl = imageUrl;

        await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return result.Created(product.Adapt<CreateProductCommandResponse>());
    }
}

