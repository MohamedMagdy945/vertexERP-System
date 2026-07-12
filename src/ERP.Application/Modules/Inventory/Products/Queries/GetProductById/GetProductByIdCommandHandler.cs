using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Queries.GetProductById;


public class GetProductByIdCommandHandler
    : IRequestHandler<GetProductByIdCommand, Result<GetProductByIdCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetProductByIdCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public GetProductByIdCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<GetProductByIdCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _dbContext = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<GetProductByIdCommandResponse>> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FindAsync(request.Id, cancellationToken);
        if (product == null)
        {
            return Result<GetProductByIdCommandResponse>.NotFound($"Product with Id {request.Id} not found.");
        }

        return Result<GetProductByIdCommandResponse>.Success(new GetProductByIdCommandResponse
        {
            Id = product.Id,
            Name = product.Name,
            Code = product.Code,
            Description = product.Description,
            SellingPrice = product.SellingPrice,
            ImageUrl = product.ImageUrl,
        }, "Product found successfully.");

    }
}

