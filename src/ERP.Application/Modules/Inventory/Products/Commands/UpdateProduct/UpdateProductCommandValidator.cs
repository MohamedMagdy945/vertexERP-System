using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{

    public UpdateProductCommandValidator()
    {

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.Code)
          .NotEmpty().WithMessage("Product code is required.")
          .MaximumLength(50).WithMessage("Product code must not exceed 50 characters.");

        RuleFor(x => x.CostPrice)
            .GreaterThan(0).WithMessage("Cost price must be greater than 0.");

        RuleFor(x => x.SellingPrice)
             .GreaterThan(0).WithMessage("Selling price must be greater than 0.");


        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("A valid category must be selected.");

        RuleFor(x => x.Unit)
            .IsInEnum().WithMessage("The selected unit type is invalid.");


    }

}