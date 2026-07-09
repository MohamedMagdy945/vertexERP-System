namespace VertexERP.Application.Abstractions.Authentication;

public interface IClientInfoProvider
{
    string GetIpAddress();
    string GetUserAgent();
}

