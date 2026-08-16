using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Roles.Create;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {

        var invalidPermissions = request.Permissions
           .Except(SecurityPerms.All)
           .ToArray();

        if (invalidPermissions.Length > 0)
        {
            return Result<Response>.BadRequest($"Invalid permissions: {string.Join(", ", invalidPermissions)}");
        }
        var role = new Role(request.Name);

        foreach (var permission in request.Permissions.Distinct())
        {
            role.AddPermission(permission);
        }

        dbContext.Roles.Add(role);

        await dbContext.SaveChangesAsync(ct);

        return Result<Response>.Success(new Response
        {
            Id = role.Id,
            Name = role.Name,
            Permissions = role.RolePermissions
            .Select(rp => rp.Permission)
            .ToList()

        });
    }
}