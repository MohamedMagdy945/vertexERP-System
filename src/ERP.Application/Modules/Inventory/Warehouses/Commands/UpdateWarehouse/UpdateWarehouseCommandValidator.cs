using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.UpdateWarehouse;

public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{

    public UpdateWarehouseCommandValidator()
    {

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(20).WithMessage("Name max length is 20");

        RuleFor(x => x.Code)
          .NotEmpty().WithMessage("Code cannot be empty")
          .MaximumLength(20).WithMessage("Code max length is 20");

        RuleFor(x => x.Location)
          .NotEmpty().WithMessage("Location cannot be empty")
          .MaximumLength(20).WithMessage("Location max length is 20");

    }

}