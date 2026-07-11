using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Authentication;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Commands.CreateUser;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, Result<CreateUserCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IPasswordHasher _passwordHasher;
    public CreateUserCommandHandler(
        IApplicationDbContext dbContext,
        IPasswordHasher passwordHasher,
        ILogger<CreateUserCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var exists = await _dbContext.Users
               .AnyAsync(x => x.Email == email, cancellationToken);

        if (exists)
            return Result<CreateUserCommandResponse>.Failure("Email already exists.");





        var permissions = await _dbContext.Permissions
           .Where(x => request.PermissionIds.Contains(x.Id))
           .Select(x => x.Id)
           .ToListAsync(cancellationToken);

        var user = request.Adapt<User>();

        user.UserPermissions = permissions
         .Select(id => new UserPermission
         {
             PermissionId = id
         })
         .ToList();

        user.PasswordHash = _passwordHasher.Hash(request.Password);

        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);


        return Result<CreateUserCommandResponse>.Success(
                  new CreateUserCommandResponse(
                      user.Id,
                      user.FirstName,
                      user.LastName,
                      user.FullName,
                      user.Email,
                      user.IsEnabled));
    }
}

