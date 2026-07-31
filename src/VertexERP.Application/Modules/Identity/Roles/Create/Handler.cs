using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Authorization;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Roles.Create;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {

        var invalidPermissions = request.Permissions
           .Except(Perms.All)
           .ToArray();

        if (invalidPermissions.Length > 0)
        {
            return Result<Response>.BadRequest(
                $"Invalid permissions: {string.Join(", ", invalidPermissions)}");
        }
        var role = new Role(request.Name);


        foreach (var permission in request.Permissions.Distinct())
        {
            role.AddPermission(permission);
        }

        dbContext.Roles.Add(role);

        try
        {
            await dbContext.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is SqlException { Number: 2601 or 2627 })
                return Result<Response>.Conflict("Role name already exists.");

            throw;
        }
        return Result<Response>.Success(new Response
        {
            Id = role.Id,
            Name = role.Name
        });
    }
}