using VertexERP.Domain.Module.Identity.Enum;

namespace VertexERP.Application.Modules.Identity.Users.Update;

public sealed record Request(
    string Name,
    string Email,
    PortalType PortalType,
    IReadOnlyCollection<Guid> RoleIds);