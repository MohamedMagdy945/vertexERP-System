using Microsoft.AspNetCore.SignalR;
using VertexERP.Application.Common.Abstractions.Notifications;

namespace VertexERP.Infrastructure.SignalR;

public sealed class NotificationPublisher(
    IHubContext<NotificationHub> hubContext) : INotificationPublisher
{
    public async Task PublishAsync(Guid userId,
      object notification, CancellationToken ct = default)
    {

        await hubContext.Clients
            .User(userId.ToString())
            .SendAsync("NotificationReceived", notification, ct);

    }
    public async Task PublishAsync(IEnumerable<Guid> userIds,
        object notification, CancellationToken ct = default)
    {
        foreach (var userId in userIds.Distinct())
        {
            await hubContext.Clients
                .User(userId.ToString())
                .SendAsync("NotificationReceived", notification, ct);
        }
    }
}