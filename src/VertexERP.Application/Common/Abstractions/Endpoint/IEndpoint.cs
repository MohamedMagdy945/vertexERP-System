using Microsoft.AspNetCore.Routing;

namespace VertexERP.Application.Common.Abstractions.Endpoint;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}