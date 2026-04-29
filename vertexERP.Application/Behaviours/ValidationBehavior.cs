using FluentValidation;
using MediatR;
using VertexERP.Application.Common.Exceptions;

namespace VertexERP.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse>
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
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .GroupBy(f => f.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).Distinct().ToList()
                );

            if (failures.Any())
            {
                throw new ValidationAppException("Invalid data", failures);
            }
        }

        return await next();
    }
}