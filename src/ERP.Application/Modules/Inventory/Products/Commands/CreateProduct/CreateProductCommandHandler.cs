using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Domain.Module.Inventory.Enums;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.CreateProduct;


public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, Result<CreateProductCommandResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public CreateProductCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<CreateProductCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _context = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<CreateProductCommandResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = Result<CreateProductCommandResponse>.Create();

        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var product = request.Adapt<Product>();
            product.Unit = (UnitType)request.Unit;

            await _context.Products.AddAsync(product, cancellationToken);

            if (request.Images is not null && request.Images.Count > 0)
            {
                var directory = $"products/{request.Code}";

                var uploadTasks = request.Images.Select(async image =>
                {
                    var imageUrl = await _fileStorage.UploadAsync(image, directory, cancellationToken);
                    return new ProductImage { Url = imageUrl };
                });

                var productImages = await Task.WhenAll(uploadTasks);
                product.Images = productImages.ToList();
            }

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            var productResponse = product.Adapt<CreateProductCommandResponse>();
            productResponse.ImagesUrl = product.Images.Select(img => img.Url).ToList();

            return response.Created(productResponse);
        }
        catch (Exception)
        {
            throw;
        }
    }
}

