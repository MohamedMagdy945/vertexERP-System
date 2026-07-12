using FluentValidation;


namespace VertexERP.Application.Modules.Inventory.Warehouses.Queries.GetWarehouseById;

public class GetWarehouseByIdCommandValidator : AbstractValidator<GetWarehouseByIdCommand>
{

    public GetWarehouseByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");
    }
}