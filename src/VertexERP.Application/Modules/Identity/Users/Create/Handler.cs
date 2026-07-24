using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Create;

public sealed class Handler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher) : IHandler
{
    private const string DefaultPassword = "P@ssw0rd123";
    public async ValueTask<Result<Response>> HandleAsync(Command request, CancellationToken cancellationToken)
    {

        var email = request.Email.Trim().ToLowerInvariant();

        var emailExists = await dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken);

        if (emailExists)
            return Result<Response>.Conflict("Email already exists.");

        var hash = passwordHasher.Hash(DefaultPassword);

        var user = new User(request.FullName, email, hash);

        dbContext.Users.Add(user);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Created(user.Adapt<Response>());
    }
}