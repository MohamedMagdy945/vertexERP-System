using VertexERP.Application.Shared.Pagination;
using VertexERP.Domain.Module.Notifications.Enum;

namespace VertexERP.Application.Modules.Notifications.GetList;

public sealed record Request(
    bool? IsRead,
    NotificationType? Type
) : PageRequest;