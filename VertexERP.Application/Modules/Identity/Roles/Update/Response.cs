namespace VertexERP.Application.Modules.Identity.Roles.Update;

public sealed record RoleResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
public sealed record Response
{
    public required Guid RoleId { get; init; }
    public required string Name { get; init; }
    public required IReadOnlySet<string> Permissions { get; init; }
}
