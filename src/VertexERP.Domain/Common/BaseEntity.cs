namespace VertexERP.Domain.Common;

public abstract class Entity : AuditableEntity
{
    public Guid Id { get; protected init; } = Guid.NewGuid();
}
