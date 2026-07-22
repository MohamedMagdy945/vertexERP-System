using FluentValidation;

namespace VertexERP.Application.Modules.Catalog.Products.Commands.Update;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Product code is required.")
            .MaximumLength(40).WithMessage("Product code must not exceed 50 characters.")
            .Matches(@"^[a-zA-Z0-9-_]+$").WithMessage("Code can only contain alphanumeric characters, hyphens, and underscores.");

        RuleFor(x => x.CostPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Cost price must be greater than or equal to 0.");


        RuleFor(x => x.SellingPrice)
            .GreaterThan(0).WithMessage("Selling price must be greater than 0.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.UnitId)
            .NotEmpty().WithMessage("Measurement unit ID is required.");

        When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
        {
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
        });

    }
}
