namespace VertexERP.Domain.Module.Notifications.Entities;

public sealed class NotificationRecipient
{
    public Guid NotificationId { get; private set; }

    public Guid UserId { get; private set; }

    public bool IsRead { get; private set; }

    public DateTime? ReadAt { get; private set; }

    public Notification Notification { get; private set; } = default!;

    public void MarkAsRead()
    {
        IsRead = true;
        ReadAt = DateTime.UtcNow;
    }
}