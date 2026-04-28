using FluentValidation;
using MediatR;
using VertexERP.Application.Common.Bases;

namespace VertexERP.Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Response<TResponse>>
            where TRequest : IRequest<Response<TResponse>>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<Response<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Response<TResponse>> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

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
                return ResponseHandler.Failure<TResponse>("Invalid input data. Please check your request and try again.", failures);
            }
            return await next();
        }
    }
}
