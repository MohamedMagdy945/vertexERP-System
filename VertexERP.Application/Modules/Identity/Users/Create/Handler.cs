using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.Create;

public sealed class Handler(IAppDbContext dbContext, IPasswordHasher passwordHasher) : IHandler
{
    private const string DefaultPassword = "P@ssw0rd123";
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var email = request.Email.ToCleanString();

        var hash = passwordHasher.Hash(DefaultPassword);

        var user = new User(
            request.Name,
            email,
            hash,
            request.PortalType);

        dbContext.Users.Add(user);

        foreach (var roleId in request.RoleIds)
        {
            user.AssignRole(roleId);
        }
        await dbContext.SaveChangesAsync(ct);

        return Result<Response>.Created(user.Adapt<Response>());
    }
}