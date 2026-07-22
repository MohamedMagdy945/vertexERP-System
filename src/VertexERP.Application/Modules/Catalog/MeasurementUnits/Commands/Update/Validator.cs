using FluentValidation;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Commands.Update;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id)
        .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.Symbol)
            .NotEmpty().WithMessage("Unit symbol is required.")
            .MaximumLength(20).WithMessage("Unit symbol must not exceed 20 characters.");
    }
}
