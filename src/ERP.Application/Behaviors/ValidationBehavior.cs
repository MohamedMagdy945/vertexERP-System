using FluentValidation;
using MediatR;
using VertexERP.Shared.Exceptions;

namespace VertexERP.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
        .SelectMany(r => r.Errors)
        .Where(f => f is not null)
        .Select(f => f.ErrorMessage)
        .ToArray();

        if (failures.Length > 0)
        {
            throw new ValidationAppException(failures);
        }

        return await next();
    }
}

