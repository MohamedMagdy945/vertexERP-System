using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Authentication;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdQueryResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetUserByIdQueryHandler> _logger;
    public GetUserByIdQueryHandler(
        IApplicationDbContext dbContext,
        ITokenGenerator tokenGenerator,
        ILogger<GetUserByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = Result<GetUserByIdQueryResponse>.Create();

        var user = await _dbContext.Users
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new GetUserByIdQueryResponse
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                IsEnabled = x.IsEnabled
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return result.NotFound("User not found.");
        }

        return result.Success(user);
    }
}

