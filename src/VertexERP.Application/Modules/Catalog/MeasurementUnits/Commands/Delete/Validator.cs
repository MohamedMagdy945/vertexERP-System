using FluentValidation;

namespace VertexERP.Application.Modules.Catalog.Units.Command.Create;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Category ID is required.");
    }
}
