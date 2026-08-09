namespace VertexERP.Application.Modules.Identity.UserRoles.Update;

public sealed record Request(Guid UserId, IReadOnlyList<Guid> RoleIds);