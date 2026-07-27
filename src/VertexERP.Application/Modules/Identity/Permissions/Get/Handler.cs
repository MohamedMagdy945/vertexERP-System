using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Authorization;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Permissions.Get;

public sealed class Handler() : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var allPermissions = SecurityPermissions.All;

        return Result<Response>.Success(new Response
        {
            Permissions = allPermissions
        });
    }
}