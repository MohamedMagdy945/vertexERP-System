using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using VertexERP.Application.Common.Filters;

namespace VertexERP.Application.Common.Extensions;

public static class ValidationExtensions
{
    public static RouteHandlerBuilder AddValidation<TRequest>(this RouteHandlerBuilder builder)
        where TRequest : class
    {
        return builder.AddEndpointFilter<ValidationFilter<TRequest>>();
    }
}