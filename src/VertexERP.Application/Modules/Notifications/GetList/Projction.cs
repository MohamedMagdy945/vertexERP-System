using VertexERP.Domain.Module.Notifications.Entities;

namespace VertexERP.Application.Modules.Notifications.GetList;

public static class Projection
{
    public static IQueryable<Response> ToResponse(
          this IQueryable<NotificationRecipient> query)
    {
        return query.Select(x => new Response
        {
            NotificationId = x.NotificationId,
            Title = x.Notification.Title,
            Message = x.Notification.Message,
            Type = x.Notification.Type,
            Data = x.Notification.Data,

            IsRead = x.IsRead,
            ReadAt = x.ReadAt,

            CreatedAt = x.CreatedAt
        });
    }
};