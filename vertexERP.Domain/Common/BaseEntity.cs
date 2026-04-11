namespace vertexERP.Domain.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    }
}
