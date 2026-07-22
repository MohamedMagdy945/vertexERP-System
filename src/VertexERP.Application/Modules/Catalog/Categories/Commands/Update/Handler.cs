using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Catalog.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categories.Commands.Update;

public sealed class Handler(IApplicationDbContext dbContext)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FindAsync([request.Id], cancellationToken);

        if (category is null)
            return Result<Response>.NotFound("Category not found.");

        var categoryName = Category.FormatName(request.Name);

        if (category.Name != categoryName)
        {
            var exists = await dbContext.Categories
                .AnyAsync(x => x.Id != request.Id && x.Name == categoryName, cancellationToken);

            if (exists)
                return Result<Response>.Conflict("Category name already exists.");
        }

        category.Update(request.Name, request.Description);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(category.Adapt<Response>());
    }
}