using VertexERP.Domain.Common;

namespace VertexERP.Infrastructure.Identity
{
    public class RefreshToken : BaseEntity
    {
        public string TokenHash { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string? RevokedReason { get; set; }
        public bool IsUsed { get; set; }
        public string? CreatedByIp { get; set; }
        public string? RevokedByIp { get; set; }
        public string? DeviceInfo { get; set; }
        public string? ReplacedByTokenHash { get; set; }
    }
}
