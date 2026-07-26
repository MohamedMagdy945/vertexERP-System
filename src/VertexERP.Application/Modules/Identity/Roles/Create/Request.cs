namespace VertexERP.Application.Modules.Identity.Roles.Create;

public sealed record Request(
    string Name,
    string? Description,
    IReadOnlyList<Guid> PermissionIds
);