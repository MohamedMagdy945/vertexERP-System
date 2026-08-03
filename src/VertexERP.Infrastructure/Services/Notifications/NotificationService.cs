using System.Text.Json;
using VertexERP.Application.Common.Abstractions.Notifications;
using VertexERP.Domain.Module.Notifications.Entities;
using VertexERP.Domain.Module.Notifications.Enum;
using VertexERP.Infrastructure.Persistence;

namespace VertexERP.Infrastructure.Services.Notifications;

public sealed class NotificationService(
    AppDbContext dbContext)
    : INotificationService
{
    public async Task SendAsync(
        IEnumerable<Guid> userIds,
        string title,
        string message,
        NotificationType type,
        object? data,
        CancellationToken ct = default)
    {
        var notification = new Notification(
            title,
            message,
            type,
            data is null ? null : JsonSerializer.Serialize(data));

        foreach (var userId in userIds.Distinct())
        {
            notification.Recipients.Add(
                new NotificationRecipient(userId));
        }

        dbContext.Notifications.Add(notification);

        await dbContext.SaveChangesAsync(ct);
    }
}