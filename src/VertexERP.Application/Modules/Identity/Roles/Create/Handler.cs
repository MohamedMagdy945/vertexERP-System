//using Microsoft.EntityFrameworkCore;
//using VertexERP.Application.Common.Abstractions.Handler;
//using VertexERP.Application.Common.Abstractions.Persistence;
//using VertexERP.Application.Shared.Results;
//using VertexERP.Domain.Module.Identity.Entities;

//namespace VertexERP.Application.Modules.Identity.Roles.Create;

//public sealed class Handler(IAppDbContext dbContext) : IHandler
//{
//    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
//    {

//        var roleExists = await dbContext.Roles
//            .AnyAsync(x => x.Name == request.Name, ct);

//        if (roleExists)
//            return Result<Response>.Failure("Role already exists.");

//        var permissions = await dbContext.Permissions
//            .Where(x => request.PermissionIds.Contains(x.Id))
//            .ToListAsync(ct);

//        if (permissions.Count != request.PermissionIds.Count)
//            return Result<Response>.Failure("Some permissions not found.");


//        var role = new Role(request.Name);

//        role.AddPermissions(permissions);

//        dbContext.Roles.Add(role);

//        await dbContext.SaveChangesAsync(ct);

//        return Result<Response>.Success(new Response
//        {
//            Id = role.Id,
//            Name = role.Name
//        });
//    }
//}