using FluentValidation;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Commands.Create;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Unit name is required.")
            .MaximumLength(50).WithMessage("Unit name must not exceed 20 characters.");

        RuleFor(x => x.Symbol)
            .NotEmpty().WithMessage("Unit symbol is required.")
            .MaximumLength(20).WithMessage("Unit symbol must not exceed 20 characters.");
    }
}
