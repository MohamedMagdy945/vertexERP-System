using Microsoft.AspNetCore.Http;
using VertexERP.Application.Abstractions.Authentication;

namespace VertexERP.Infrastructure.Authentication;

public class ClientInfoProvider : IClientInfoProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClientInfoProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetIpAddress()
    {
        return _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
        ?? "Unknown";
    }

    public string GetUserAgent()
    {
        return _httpContextAccessor.HttpContext?.Request.Headers.UserAgent.ToString()
        ?? "Unknown";
    }

}

