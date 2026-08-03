using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Notifications.Entities;

public sealed class NotificationRecipient : Entity
{
    public Guid NotificationId { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsRead { get; private set; } = false;
    public DateTime? ReadAt { get; private set; }
    public Notification Notification { get; private set; } = default!;

    private NotificationRecipient()
    {
        // Required by EF Core
    }

    public NotificationRecipient(Guid userId)
    {
        UserId = userId;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        if (IsRead)
            return;

        IsRead = true;
        ReadAt = DateTime.UtcNow;
    }
}