using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(20);

        RuleFor(x => x.Description)
            .MaximumLength(100);
    }
}