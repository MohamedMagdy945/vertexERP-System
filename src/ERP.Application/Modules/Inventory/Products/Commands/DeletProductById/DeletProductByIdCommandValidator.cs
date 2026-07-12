using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.DeletProduct;


public class DeletProductByIdCommandValidator : AbstractValidator<DeletProductByIdCommand>
{

    public DeletProductByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");
    }
}