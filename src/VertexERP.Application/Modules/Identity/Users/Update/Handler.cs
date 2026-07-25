using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Update;

public sealed class Handler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();



        var emailExists = await dbContext.Users
            .AnyAsync(u => u.Email == email, cancellationToken);

        if (emailExists)
            return Result<Response>.Conflict("Email already exists.");


        user.AssignRole(defaultRole.Id);

        dbContext.Users.Add(user);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Created(user.Adapt<Response>());
    }
}