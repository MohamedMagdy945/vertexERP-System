namespace VertexERP.Application.Modules.Identity.Users.Update;

public sealed class Response
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PortalType { get; set; } = default!;
}