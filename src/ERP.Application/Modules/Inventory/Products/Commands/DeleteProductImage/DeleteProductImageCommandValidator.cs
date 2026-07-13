using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.DeleteProductImage;


public class DeleteProductImageCommandValidator : AbstractValidator<DeleteProductImageCommand>
{

    public DeleteProductImageCommandValidator()
    {
        RuleFor(x => x.ImageId)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");
    }
}