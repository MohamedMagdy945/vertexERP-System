using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.Create;

public sealed class Handler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher) : IHandler
{
    private const string DefaultPassword = "P@ssw0rd123";
    public async Task<Result<Response>> HandleAsync(Command request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var defaultRole = await dbContext.Roles
            .SingleOrDefaultAsync(r => r.Name == Roles.User, cancellationToken);

        if (defaultRole is null)
            return Result<Response>.Failure("Default role not found.");

        var emailExists = await dbContext.Users
            .AnyAsync(u => u.Email == email, cancellationToken);

        if (emailExists)
            return Result<Response>.Conflict("Email already exists.");

        var hash = passwordHasher.Hash(DefaultPassword);

        var user = new User(request.Name, email, hash, request.PortalType);

        user.AssignRole(defaultRole.Id);

        dbContext.Users.Add(user);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Created(user.Adapt<Response>());
    }
}