using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Queries.GetById;

public sealed class Handler(IApplicationDbContext dbContext)
    : IRequestHandler<Query, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.AsNoTracking().Where(x => x.Id == request.Id)
            .ProjectToType<Response>().FirstOrDefaultAsync(cancellationToken);

        if (product is null)
            return Result<Response>.NotFound("Product not found.");

        return Result<Response>.Success(product);
    }
}