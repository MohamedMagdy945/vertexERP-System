using FluentValidation;

namespace VertexERP.Application.Modules.Catalog.Categorys.Commands.Update;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id)
        .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required.")
        .MaximumLength(30).WithMessage("Name must not exceed 30 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
    }
}
