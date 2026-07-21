using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Categorys.Command.Create;

public sealed class Handler(IApplicationDbContext dbContext, ILogger<Handler> logger)
    : IRequestHandler<Query, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var exists = await dbContext.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (exists)
            return Result<Response>.Conflict("Category name already exists.");

        var category = new Category(request.Name, request.Description);

        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Created(category.Adapt<Response>());
    }
}