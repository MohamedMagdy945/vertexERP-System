using FluentValidation;
using VertexERP.Application.Modules.Catalog.Products.Queries.Get;

namespace VertexERP.Application.Modules.Catalog.Products.Queries.GetById;

public sealed class Validator : AbstractValidator<Query>
{
    public Validator()
    {
        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Category ID is required.");
    }
}
