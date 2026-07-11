using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Queries.GetProducts;

public class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, Result<PagedResult<GetProductsQueryResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetProductsQueryHandler> _logger;
    public GetProductsQueryHandler(
        IApplicationDbContext dbContext,
        ILogger<GetProductsQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<PagedResult<GetProductsQueryResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {

        var query = _dbContext.Categories
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var categories = await query
            .OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetProductsQueryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<GetProductsQueryResponse>
        {
            Items = categories,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        return Result<PagedResult<GetProductsQueryResponse>>.Success(result);
    }
}

