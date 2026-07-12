using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Queries.GetWarehourseById;


public class GetProductByIdCommandValidator : AbstractValidator<GetProductByIdCommand>
{

    public GetProductByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");
    }
}