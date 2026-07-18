using System.ComponentModel.DataAnnotations;
using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Identity.Entities;

public sealed class RefreshToken : BaseEntity
{
    public string TokenHash { get; private set; } = default!;

    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public string? RevokedReason { get; private set; }

    public bool IsUsed { get; private set; }

    public string? CreatedByIp { get; private set; }
    public string? RevokedByIp { get; private set; }
    public string? DeviceInfo { get; private set; }

    public string? ReplacedByTokenHash { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;

    [Timestamp]
    public byte[] RowVersion { get; private set; } = default!;



    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt.HasValue;
    public bool IsActive => !IsExpired && !IsRevoked && !IsUsed;

    private RefreshToken() { }

    public RefreshToken(Guid userId, string tokenHash, DateTime expiresAt, string? createdByIp = null,
        string? deviceInfo = null)
    {
        TokenHash = tokenHash;
        UserId = userId;
        ExpiresAt = expiresAt;
        CreatedByIp = createdByIp;
        DeviceInfo = deviceInfo;
    }

    public void MarkAsUsed()
    {
        IsUsed = true;
    }

    public void Revoke(string? reason = null, string? revokedByIp = null, string? replacedByTokenHash = null)
    {
        if (IsRevoked)
            return;

        RevokedAt = DateTime.UtcNow;
        RevokedReason = reason;
        RevokedByIp = revokedByIp;
        ReplacedByTokenHash = replacedByTokenHash;
    }
}