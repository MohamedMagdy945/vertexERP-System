using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using VertexERP.Application.Common.Authorization;
using VertexERP.Application.Common.Filters;

namespace VertexERP.Application.Common.Extensions;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder AddValidation<TRequest>(this RouteHandlerBuilder builder)
      where TRequest : class
    {
        return builder.AddEndpointFilter<ValidationFilter<TRequest>>();
    }
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
