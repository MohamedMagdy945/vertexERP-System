namespace VertexERP.Application.Modules.Identity.UserRoles.Update;

public sealed class RoleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
public sealed class Response
{
    public Guid UserId { get; set; }
    public IReadOnlyList<RoleResponse> Roles { get; set; } = [];
}

