using Microsoft.AspNetCore.Http;
using VertexERP.Application.Common.Abstractions.Http;

namespace VertexERP.Infrastructure.Services.Http;

public sealed class ClientInfoProvider(IHttpContextAccessor httpContextAccessor) : IClientInfoProvider
{
    public string GetIpAddress()
    {
        return httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }

    public string GetUserAgent()
    {
        return httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? "Unknown";
    }
}