using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Permissions.Get;

public sealed class Handler() : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var allPermissions = SecurityPerms.All;

        return Result<Response>.Success(new Response
        {
            Permissions = allPermissions
        });
    }
}