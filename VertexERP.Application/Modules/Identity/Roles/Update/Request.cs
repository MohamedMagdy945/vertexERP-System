namespace VertexERP.Application.Modules.Identity.Roles.Update;

public sealed record Request(
    Guid Id,
    List<string> Permissions);