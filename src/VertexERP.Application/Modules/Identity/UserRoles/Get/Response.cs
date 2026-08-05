namespace VertexERP.Application.Modules.Identity.UserRoles.Get;

public sealed record Response
{
    public required Guid UserId { get; init; }
    public required IReadOnlyCollection<RoleResponse> Roles { get; init; } = [];
}

public sealed record RoleResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}