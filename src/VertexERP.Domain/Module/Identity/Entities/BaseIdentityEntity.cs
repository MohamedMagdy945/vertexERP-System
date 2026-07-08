namespace VertexERP.Domain.Module.Identity.Entities;

public class BaseIdentityEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

