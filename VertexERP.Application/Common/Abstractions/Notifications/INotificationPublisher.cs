namespace VertexERP.Application.Common.Abstractions.Notifications;

public interface INotificationPublisher
{
    Task PublishAsync(Guid userId, object notification, CancellationToken ct = default);
    Task PublishAsync(IEnumerable<Guid> userIds, object notification, CancellationToken ct = default);
}