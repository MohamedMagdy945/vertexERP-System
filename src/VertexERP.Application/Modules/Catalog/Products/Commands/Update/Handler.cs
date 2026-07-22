using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Commands.Update;

public sealed class Handler(IApplicationDbContext dbContext)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
            return Result<Response>.NotFound("Product not found.");

        if (product.Code != request.Code)
        {
            var exists = await dbContext.Products.AnyAsync(x => x.Code == request.Code, cancellationToken);

            if (exists)
                return Result<Response>.Conflict("Product code already exists.");
        }

        product.Update(request.Name, request.Code, request.CostPrice, request.SellingPrice, request.CategoryId, request.UnitId, request.Description);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(product.Adapt<Response>());
    }
}