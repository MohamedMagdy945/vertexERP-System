using VertexERP.Domain.Common;
using VertexERP.Domain.Module.Notifications.Enum;

namespace VertexERP.Domain.Module.Notifications.Entities;

public sealed class Notification : Entity
{
    public string Title { get; private set; } = default!;

    public string Message { get; private set; } = default!;

    public NotificationType Type { get; private set; }

    public string? Data { get; private set; }

    public ICollection<NotificationRecipient> Recipients { get; } = [];

    private Notification()
    {
    }

    public Notification(
        string title,
        string message,
        NotificationType type,
        string? data = null)
    {
        Title = title;
        Message = message;
        Type = type;
        Data = data;
    }
}