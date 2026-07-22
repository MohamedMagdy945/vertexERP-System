using FluentValidation;

namespace VertexERP.Application.Modules.Catalog.Products.Commands.Delete;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Product ID is required.");
    }
}
