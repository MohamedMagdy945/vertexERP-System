namespace VertexERP.Application.Modules.Identity.Roles.GetById;

public sealed class Response
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public IReadOnlySet<string> Permissions { get; init; } = new HashSet<string>();
}
