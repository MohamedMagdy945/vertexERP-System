namespace VertexERP.Application.Modules.Identity.Users.Roles.Get;

public sealed record Response
{
    public IReadOnlyCollection<RoleResponse> Roles { get; init; } = [];
}

public sealed record RoleResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
}
