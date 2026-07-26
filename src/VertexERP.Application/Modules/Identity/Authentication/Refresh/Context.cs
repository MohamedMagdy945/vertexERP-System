using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed class Context
{
    public RefreshToken RefreshToken { get; init; } = default!;
    public Guid UserId { get; init; }
    public string UserEmail { get; init; } = default!;
    public string PortalType { get; init; } = default!;
}