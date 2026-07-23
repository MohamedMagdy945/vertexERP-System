namespace VertexERP.Application.Common.Filters;

using FluentValidation;
using Microsoft.AspNetCore.Http;
using VertexERP.Application.Common.Extensions;
using VertexERP.Shared.Results;

public sealed class ValidationFilter<TRequest>(IValidator<TRequest>? validator = null)
    : IEndpointFilter where TRequest : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (validator is null)
            return await next(context);

        var request = context.Arguments.OfType<TRequest>().FirstOrDefault();

        if (request is null)
            return await next(context);

        var validationResult = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            IReadOnlyList<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            var result = Result<string>.ValidationFailed(errors);

            return result.ToMinimalResult();
        }

        return await next(context);
    }
}