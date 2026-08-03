using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Authorization;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.Create;

public sealed class Handler(IAppDbContext dbContext, IPasswordHasher passwordHasher) : IHandler
{
    private const string DefaultPassword = "P@ssw0rd123";
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var emailExists = await dbContext.Users
            .AnyAsync(u => u.Email == email, ct);

        if (emailExists)
            return Result<Response>.Conflict("Email already exists.");

        var defaultRole = await dbContext.Roles
            .SingleOrDefaultAsync(r => r.Name == SecurityRoles.User, ct);

        if (defaultRole is null)
            return Result<Response>.Failure("Default role not found.");

        var hash = passwordHasher.Hash(DefaultPassword);

        var user = new User(request.Name, email, hash, request.PortalType);

        user.AssignRole(defaultRole.Id);

        dbContext.Users.Add(user);

        await dbContext.SaveChangesAsync(ct);

        return Result<Response>.Created(user.Adapt<Response>());
    }
}