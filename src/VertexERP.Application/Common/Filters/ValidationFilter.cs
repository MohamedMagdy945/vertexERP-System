using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Common.Filters;

public sealed class ValidationFilter<TRequest>(IValidator<TRequest>? validator)
    : IEndpointFilter
    where TRequest : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        if (validator is null)
            return await next(context);

        var request = context.GetArgument<TRequest>(0);

        var validationResult = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        return await next(context);
    }
}