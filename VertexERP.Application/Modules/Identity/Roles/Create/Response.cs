namespace VertexERP.Application.Modules.Identity.Roles.Create;

public sealed record Response
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public IReadOnlyList<string> Permissions { get; set; } = [];
}
