using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Authentication;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Queries.GetUsersQuery;

public class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, Result<PagedResult<GetUsersQueryResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetUsersQueryHandler> _logger;
    private readonly IPasswordHasher _passwordHasher;
    public GetUsersQueryHandler(
        IApplicationDbContext dbContext,
        ITokenGenerator tokenGenerator,
        ILogger<GetUsersQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<PagedResult<GetUsersQueryResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {

        var query = _dbContext.Users
            .AsNoTracking();

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

        var result = new PagedResult<GetUsersQueryResponse>
        {
            Items = users,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        return Result<PagedResult<GetUsersQueryResponse>>.Success(result);
    }
}

