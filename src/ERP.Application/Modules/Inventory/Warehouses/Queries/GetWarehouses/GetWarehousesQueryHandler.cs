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

        var query = _dbContext.Warehouses
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var warehouses = await query
            .OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetWarehousesQueryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Location = x.Location,
            })
            .ToListAsync(cancellationToken);

        var result = PagedResult<GetWarehousesQueryResponse>.Create(
            warehouses, totalCount, request.PageNumber, request.PageSize);

        return Result<PagedResult<GetWarehousesQueryResponse>>.Success(result);
    }
}

