namespace VertexERP.Application.Common.Abstractions.Http;

public interface IClientInfoProvider
{
    string GetIpAddress();
    string GetUserAgent();
}