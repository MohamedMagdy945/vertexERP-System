using Mediator;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categorys.Commands.Delete;

public sealed class Handler(IApplicationDbContext dbContext, ILogger<Handler> logger)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FindAsync([request.Id], cancellationToken);

        if (category is null)
            return Result<Response>.NotFound("Category not found.");

        dbContext.Categories.Remove(category);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(new Response(category.Id));
    }
}