using VertexERP.Domain.Module.Identity.Enum;

namespace VertexERP.Application.Modules.Identity.Users.Create;

public sealed record Command(string FullName, string Email, PortalType Portal);