using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace VertexERP.Infrastructure.SignalR;

[Authorize]
public class NotificationHub : Hub
{

}

