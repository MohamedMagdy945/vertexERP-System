using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Commands.Create;


public sealed class Handler(
    IApplicationDbContext dbContext,
    IPasswordHasher passwordHasher)
    : IRequestHandler<Command, Result<Response>>
{
    private const string DefaultPassword = "P@ssw0rd123";
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var emailExists = await dbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (emailExists)
            return Result<Response>.Failure("Email already exists.");


        var user = new User(request.FirstName, request.LastName, request.Email, passwordHasher.Hash(DefaultPassword));

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(user.Adapt<Response>());
    }
}