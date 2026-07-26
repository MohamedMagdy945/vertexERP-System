namespace VertexERP.Application.Modules.Identity.Users.Roles.Update;

public sealed record Request(Guid Id, IReadOnlyList<Guid> RoleIds);