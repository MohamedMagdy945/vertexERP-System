using Microsoft.AspNetCore.Authorization;

namespace VertexERP.Application.Common.Authorization;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}

