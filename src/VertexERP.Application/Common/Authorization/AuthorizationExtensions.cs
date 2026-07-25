using Microsoft.AspNetCore.Builder;

namespace VertexERP.Application.Common.Authorization;

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
