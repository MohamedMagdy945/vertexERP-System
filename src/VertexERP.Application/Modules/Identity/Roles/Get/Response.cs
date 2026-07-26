using VertexERP.Application.Shared.Pagination;

namespace VertexERP.Application.Modules.Identity.Roles.Get;

public sealed class RoleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int UsersCount { get; set; }
}
public sealed class Response
{
    public Page<RoleResponse> Roles { get; set; } = default!;
}