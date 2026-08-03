using VertexERP.Domain.Module.Notifications.Enum;

namespace VertexERP.Application.Common.Abstractions.Notifications;

public interface INotificationService
{
    Task SendAsync(
    Guid userId,
    string title,
    string message,
    NotificationType type,
    object? data = null,
    CancellationToken ct = default);

    Task SendAsync(
        IEnumerable<Guid> userIds,
        string title,
        string message,
        NotificationType type,
        object? data = null,
        CancellationToken ct = default);
}