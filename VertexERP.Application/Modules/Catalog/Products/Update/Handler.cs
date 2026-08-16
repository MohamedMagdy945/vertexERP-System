using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Update;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid id ,Request request, CancellationToken ct)
    {
        var affectedRows = await dbContext.Products
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(setters =>
            {
                setters.SetProperty(x => x.Name, request.Name)
                       .SetProperty(x => x.Code, request.Code)
                       .SetProperty(x => x.CostPrice, request.CostPrice)
                       .SetProperty(x => x.SellingPrice, request.SellingPrice)
                       .SetProperty(x => x.CategoryId, request.CategoryId)
                       .SetProperty(x => x.UnitId, request.UnitId)
                       .SetProperty(x => x.Description, request.Description)
                       .SetProperty(x => x.Barcode, request.Barcode);
            }, ct);

        if (affectedRows == 0)
            return Result<Response>.NotFound("Product not found.");

        var response = request.Adapt<Response>();

        return Result<Response>.Success(response);
    }
}