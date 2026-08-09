using VertexERP.Domain.Module.Identity.Enum;

namespace VertexERP.Application.Modules.Identity.Users.Update;

public sealed record Request(Guid Id, string Name, PortalType PortalType);