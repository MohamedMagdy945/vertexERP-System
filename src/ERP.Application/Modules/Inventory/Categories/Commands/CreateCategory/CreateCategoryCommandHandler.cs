using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Categories.Commands.CreateCategory;


public class CreateCategoryCommandHandler
    : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<CreateCategoryCommandHandler> _logger;
    public CreateCategoryCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<CreateCategoryCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<CreateCategoryCommandResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.Categories
               .AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (exists)
            return Result<CreateCategoryCommandResponse>
                .Failure($"Category with name '{request.Name}' already exists.");

        var category = request.Adapt<Category>();
        await _dbContext.Categories.AddAsync(category, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<CreateCategoryCommandResponse>.Success(
            category.Adapt<CreateCategoryCommandResponse>(),
            "Category created successfully.");
    }
}

