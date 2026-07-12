using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.DeleteWarehouseById;


public class DeleteWarehouseByIdCommandValidator : AbstractValidator<DeleteWarehouseByIdCommand>
{

    public DeleteWarehouseByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");
    }
}