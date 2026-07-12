using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Queries.GetUsersQuery;

public class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, Result<Page<GetUsersQueryResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetUsersQueryHandler> _logger;
    public GetUsersQueryHandler(
        IApplicationDbContext dbContext,
        ILogger<GetUsersQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Page<GetUsersQueryResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {

        var result = Result<Page<GetUsersQueryResponse>>.Create();

        var query = _dbContext.Users.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var users = await query
            .OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetUsersQueryResponse
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                IsEnabled = x.IsEnabled
            })
            .ToListAsync(cancellationToken);

        var pagedData = Page<GetUsersQueryResponse>.Create(users, totalCount, request.PageNumber, request.PageSize);

        return result.Success(pagedData);
    }
}

