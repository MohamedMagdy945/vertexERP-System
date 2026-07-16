namespace VertexERP.Application.Common.Abstractions.System;

public interface IClientInfoProvider
{
    string GetIpAddress();
    string GetUserAgent();
}