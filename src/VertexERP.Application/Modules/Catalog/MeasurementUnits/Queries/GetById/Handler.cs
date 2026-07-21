using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Units.Queries.GetById;

public sealed class Handler(IApplicationDbContext dbContext, ILogger<Handler> logger)
    : IRequestHandler<Query, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.AsNoTracking().Where(x => x.Id == request.Id)
            .ProjectToType<Response>().FirstOrDefaultAsync(cancellationToken);


        if (category is null)
            return Result<Response>.NotFound("Category not found.");

        return Result<Response>.Success(category);
    }
}