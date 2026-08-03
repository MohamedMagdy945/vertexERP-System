using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Pagination;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Notifications.GetList;

public sealed class Handler(IAppDbContext dbContext, ICurrentUser currentUser) : IHandler
{
    public async Task<Result<Page<Response>>> HandleAsync(Request request, CancellationToken ct)
    {
        var query = dbContext.NotificationRecipients
          .AsNoTracking()
          .Where(x => x.UserId == currentUser.UserId);

        if (request.IsRead.HasValue)
        {
            query = query.Where(x => x.IsRead == request.IsRead.Value);
        }

        if (request.Type.HasValue)
        {
            query = query.Where(x => x.Notification.Type == request.Type.Value);
        }

        var page = await query
            .OrderByDescending(x => x.CreatedAt)
            .ToResponse()
            .ToPageAsync(request, ct);

        return Result<Page<Response>>.Success(page);
    }

}