using FluentValidation;

namespace VertexERP.Application.Modules.Catalog.Categories.Commands.Delete;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Category ID is required.");
    }
}
