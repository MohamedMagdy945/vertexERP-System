using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Update;

public sealed class Validator : AbstractValidator<Request>
{
    public Validator()
    {

        RuleFor(x => x.Name)
             .NotEmpty()
             .WithMessage("Warehouse name is required.")
             .MaximumLength(100)
             .WithMessage("Warehouse name must not exceed 100 characters.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Warehouse code is required.")
            .MaximumLength(50)
            .WithMessage("Warehouse code must not exceed 50 characters.");

        RuleFor(x => x.Location)
            .NotEmpty()
            .WithMessage("Warehouse location is required.")
            .MaximumLength(200)
            .WithMessage("Warehouse location must not exceed 200 characters.");
    }

}
