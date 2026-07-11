namespace VertexERP.Domain.Common;

public class AuditableEntity
{
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
}
