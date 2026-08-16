namespace VertexERP.Application.Modules.Identity.UserRoles.Update;


public sealed record Response(
    Guid UserId,
    IReadOnlyList<Guid> RoleIds
);