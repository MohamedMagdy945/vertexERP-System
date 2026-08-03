using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VertexERP.Application.Common.Abstractions.Identity;

namespace VertexERP.Infrastructure.Services.Identity;

public sealed class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUser
{
    private ClaimsPrincipal User => accessor.HttpContext?.User ?? new ClaimsPrincipal();

    public Guid UserId => Guid.TryParse
            (User.FindFirstValue(JwtRegisteredClaimNames.Sub), out var id) ? id : Guid.Empty;

    public string? Email => User.FindFirstValue(JwtRegisteredClaimNames.Email);

    public string? Name => User.FindFirstValue(JwtRegisteredClaimNames.Name);

    public bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;

    public bool IsInRole(string role) => User.IsInRole(role);
}