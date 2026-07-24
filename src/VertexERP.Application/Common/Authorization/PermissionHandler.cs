using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Shared.Constant;

namespace VertexERP.Application.Common.Authorization;

public sealed class PermissionHandler(IUserPermissionService userPermissionService, IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User.IsInRole(Roles.System))
        {
            context.Succeed(requirement);
            return;
        }

        var ct = httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;

        var userIdClaim = context.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            return;

        var permissions = await userPermissionService.GetPermissionsAsync(userId, ct);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}