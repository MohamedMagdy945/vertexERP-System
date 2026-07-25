namespace VertexERP.Application.Modules.Identity.Users.Create;

public sealed record Response(
    Guid Id,
    string Name,
    string Email,
    string PortalType);