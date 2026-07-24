using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace VertexERP.Application.Common.Authorization;

public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = new AuthorizationPolicyBuilder().AddRequirements(new PermissionRequirement(policyName)).Build();

        return Task.FromResult<AuthorizationPolicy?>(policy);
    }
}