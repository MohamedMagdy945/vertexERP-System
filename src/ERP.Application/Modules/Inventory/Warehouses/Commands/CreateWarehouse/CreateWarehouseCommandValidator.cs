using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.CreateWarehouse;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{

    public CreateWarehouseCommandValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(150).WithMessage("Product name must not exceed 150 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Product code is required.")
            .MaximumLength(50).WithMessage("Product code must not exceed 50 characters.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Product location is required.")
            .MaximumLength(50).WithMessage("Product location must not exceed 50 characters.");
    }
}