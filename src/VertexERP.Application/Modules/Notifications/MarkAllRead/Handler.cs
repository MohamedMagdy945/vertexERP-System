using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Notifications.MarkAllRead;

public sealed class Handler(IAppDbContext dbContext, ICurrentUser currentUser) : IHandler
{
    public async Task<Result<Response>> HandleAsync(CancellationToken ct)
    {
        await dbContext.NotificationRecipients
            .Where(x => x.UserId == currentUser.UserId && !x.IsRead)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.IsRead, true)
                    .SetProperty(x => x.ReadAt, DateTime.UtcNow),
                ct);

        return Result<Response>.Success(new Response());
    }

}