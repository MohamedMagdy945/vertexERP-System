namespace VertexERP.Application.Modules.Identity.Roles.Get;

public sealed class Response
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int UsersCount { get; set; }
}
