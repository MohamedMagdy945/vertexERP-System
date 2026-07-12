using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Queries.GetWarehouses;

public class GetWarehousesQueryHandler
    : IRequestHandler<GetWarehousesQuery, Result<PagedResult<GetWarehousesQueryResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetWarehousesQueryHandler> _logger;
    public GetWarehousesQueryHandler(
        IApplicationDbContext dbContext,
        ILogger<GetWarehousesQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<PagedResult<GetWarehousesQueryResponse>>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
    {

        var query = _dbContext.Products
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var categories = await query
            .OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetWarehousesQueryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Code = x.Code,
                SellingPrice = x.SellingPrice
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<GetWarehousesQueryResponse>
        {
            Items = categories,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        return Result<PagedResult<GetWarehousesQueryResponse>>.Success(result);
    }
}

