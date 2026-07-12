using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Modules.Identity.Users.Queries.GetCategories;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler
    : IRequestHandler<GetCategoriesQuery, Result<PagedResult<GetCategoriesQueryResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetCategoriesQueryHandler> _logger;
    public GetCategoriesQueryHandler(
        IApplicationDbContext dbContext,
        ILogger<GetCategoriesQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<PagedResult<GetCategoriesQueryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {

        var query = _dbContext.Categories
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var categories = await query
            .OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetCategoriesQueryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            })
            .ToListAsync(cancellationToken);

        var result = PagedResult<GetCategoriesQueryResponse>.Create(
            categories, totalCount, request.PageNumber, request.PageSize);

        return Result<PagedResult<GetCategoriesQueryResponse>>.Success(result);
    }
}

