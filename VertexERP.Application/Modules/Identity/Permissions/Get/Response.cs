namespace VertexERP.Application.Modules.Identity.Permissions.Get;

public sealed class Response
{
    public IReadOnlySet<string> Permissions { get; set; } = default!;
}