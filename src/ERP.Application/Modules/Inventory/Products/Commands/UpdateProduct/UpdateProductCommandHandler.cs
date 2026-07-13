using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        var response = Result<UpdateProductCommandResponse>.Create();


        var rowsAffected = await _dbContext.Products
          .Where(x => x.Id == request.Id)
          .ExecuteUpdateAsync(x => x
              .SetProperty(p => p.Name, request.Name)
              .SetProperty(p => p.SellingPrice, request.SellingPrice)
              .SetProperty(p => p.CostPrice, request.CostPrice)
              .SetProperty(p => p.Barcode, request.Barcode)
              .SetProperty(p => p.Code, request.Code)
              .SetProperty(p => p.Description, request.Description)
              .SetProperty(p => p.Unit, request.Unit)
              .SetProperty(p => p.CategoryId, request.CategoryId), cancellationToken);

        if (rowsAffected == 0)
            return response.NotFound();

        return response.Updated(request.Adapt<UpdateProductCommandResponse>());
    }
}

