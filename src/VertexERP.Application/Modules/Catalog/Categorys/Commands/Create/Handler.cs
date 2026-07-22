using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Catalog.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categorys.Commands.Create;

public sealed class Handler(IApplicationDbContext dbContext, ILogger<Handler> logger)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var categoryName = Category.FormatName(request.Name);

        var exists = await dbContext.Categories.AnyAsync(x => x.Name == categoryName, cancellationToken);

        if (exists)
            return Result<Response>.Conflict("Category name already exists.");

        var category = new Category(request.Name, request.Description);
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Created(category.Adapt<Response>());
    }
}