//using Microsoft.EntityFrameworkCore;
//using VertexERP.Application.Common.Abstractions.Handler;
//using VertexERP.Application.Common.Abstractions.Persistence;
//using VertexERP.Application.Common.Extensions;
//using VertexERP.Application.Shared.Results;

//namespace VertexERP.Application.Modules.Identity.Permissions.Get;

//public sealed class Handler(IAppDbContext dbContext) : IHandler
//{
//    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
//    {
//        var query = dbContext.Users.AsNoTracking();

//        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
//        {
//            var term = $"%{request.SearchTerm.Trim()}%";

//            query = query.Where(x => EF.Functions.Like(x.Email, term));
//        }

//        var page = await query
//            .OrderBy(x => x.Id)
//            .ToResponse()
//            .ToPagedAsync(request, ct);


//        return Result<Response>.Success(new Response { Users = page });
//    }
//}
//using Microsoft.EntityFrameworkCore;
//using VertexERP.Application.Common.Abstractions.Handler;
//using VertexERP.Application.Common.Abstractions.Persistence;
//using VertexERP.Application.Common.Extensions;
//using VertexERP.Application.Shared.Results;

//namespace VertexERP.Application.Modules.Identity.Permissions.Get;

//public sealed class Handler(IAppDbContext dbContext) : IHandler
//{
//    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
//    {
//        var query = dbContext.Users.AsNoTracking();

//        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
//        {
//            var term = $"%{request.SearchTerm.Trim()}%";

//            query = query.Where(x => EF.Functions.Like(x.Email, term));
//        }

//        var page = await query
//            .OrderBy(x => x.Id)
//            .ToResponse()
//            .ToPagedAsync(request, ct);


//        return Result<Response>.Success(new Response { Users = page });
//    }
//}