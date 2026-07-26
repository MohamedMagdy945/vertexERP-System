namespace VertexERP.Application.Modules.Identity.Roles.Create;

public sealed record Response
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public IReadOnlyList<PermissionResponse> Permissions { get; set; } = [];
}

public sealed record PermissionResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}