using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Categorys.Command.Update;

public sealed class Handler(IApplicationDbContext dbContext, ILogger<Handler> logger)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (category is null)
            return Result<Response>.NotFound("Category not found.");

        if (category.Name != request.Name)
        {
            var exists = await dbContext.Categories
                .AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (exists)
                return Result<Response>.Conflict("Category name already exists.");
        }

        category.Update(request.Name, request.Description);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(category.Adapt<Response>());
    }
}