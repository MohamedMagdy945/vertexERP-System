using Mediator;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Commands.Delete;

public sealed class Handler(IApplicationDbContext dbContext)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([request.Id], cancellationToken);

        if (product is null)
            return Result<Response>.NotFound("Product not found.");

        dbContext.Products.Remove(product);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(new Response(product.Id));
    }
}