using VertexERP.Domain.Module.Notifications.Enum;

namespace VertexERP.Application.Modules.Notifications.GetList;

public sealed class Response
{
    public Guid NotificationId { get; init; }
    public string Title { get; init; } = default!;
    public string Message { get; init; } = default!;
    public NotificationType Type { get; init; }
    public bool IsRead { get; init; }
    public DateTime? ReadAt { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? Data { get; init; }
}