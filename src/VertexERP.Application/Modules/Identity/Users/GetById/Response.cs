namespace VertexERP.Application.Modules.Identity.Users.GetById;

public sealed class Response
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
    public string PortalType { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public IReadOnlyList<string> Roles { get; set; } = [];
    public IReadOnlySet<string>? Permissions { get; set; }
}