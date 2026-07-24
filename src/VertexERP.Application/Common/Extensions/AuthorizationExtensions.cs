using Microsoft.AspNetCore.Builder;
using VertexERP.Application.Common.Authorization;

namespace VertexERP.Application.Common.Extensions;

public static class AuthorizationExtensions
{
    public static RouteHandlerBuilder RequireRole(this RouteHandlerBuilder builder, params string[] roles)
    {
        return builder.RequireAuthorization(policy => policy.RequireRole(roles));
    }
    public static RouteHandlerBuilder HasPermission(this RouteHandlerBuilder builder, string permission)
    {
        return builder.RequireAuthorization(policy =>
            policy.AddRequirements(new PermissionRequirement(permission)));
    }
}
