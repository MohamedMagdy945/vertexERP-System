using FluentValidation;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Create;

public sealed class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Symbol)
          .NotEmpty().WithMessage("Unit symbol is required.")
          .MaximumLength(20).WithMessage("Unit symbol must not exceed 20 characters.");
    }
}