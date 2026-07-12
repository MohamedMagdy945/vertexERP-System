using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Queries.GetProducts;

public class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, Result<Page<GetProductsQueryResponse>>>
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

    public async Task<Result<Page<GetProductsQueryResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result = Result<Page<GetProductsQueryResponse>>.Create();

        var query = _dbContext.Products.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var products = await query
            .OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetProductsQueryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Code = x.Code,
                SellingPrice = x.SellingPrice
            })
            .ToListAsync(cancellationToken);

        var page = Page<GetProductsQueryResponse>.Create(
         products, totalCount, request.PageNumber, request.PageSize);

        return result.Success(page);
    }
}

