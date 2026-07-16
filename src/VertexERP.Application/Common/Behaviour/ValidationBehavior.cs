using FluentValidation;
using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Common.Behaviour;

public sealed class GenericValidationBehavior<TRequest, TValue>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, Result<TValue>>
    where TRequest : IMessage
{
    public async ValueTask<Result<TValue>> Handle(
        TRequest message,
        MessageHandlerDelegate<TRequest, Result<TValue>> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next(message, cancellationToken);
        }

        var context = new ValidationContext<TRequest>(message);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .Select(f => f.ErrorMessage)
            .Distinct()
            .ToArray();

        if (errors.Length > 0)
        {
            return Result<TValue>.ValidationFailed(errors);
        }

        return await next(message, cancellationToken);
    }
}