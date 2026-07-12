using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Authentication;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Permissions.Queries.GetAllPermissions;

public class GetAllPermissionsQueryHandler
    : IRequestHandler<GetAllPermissionsQuery, Result<List<GetAllPermissionsQueryResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetAllPermissionsQueryHandler> _logger;
    private readonly IPasswordHasher _passwordHasher;
    public GetAllPermissionsQueryHandler(
        IApplicationDbContext dbContext,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher,
        ILogger<GetAllPermissionsQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<List<GetAllPermissionsQueryResponse>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var result = Result<List<GetAllPermissionsQueryResponse>>.Create();

        var permissions = await _dbContext.Permissions
            .Select(x => new GetAllPermissionsQueryResponse
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync(cancellationToken);

        return result.Success(permissions, "Permissions retrieved successfully");


    }
}

