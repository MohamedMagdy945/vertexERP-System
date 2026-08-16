namespace VertexERP.Application.Modules.Identity.UserRoles.Update;

public sealed record Request(
    IReadOnlyList<Guid> RoleIds
);