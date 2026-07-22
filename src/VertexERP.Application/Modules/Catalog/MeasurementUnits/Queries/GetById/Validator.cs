using FluentValidation;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Queries.GetById;

public sealed class Validator : AbstractValidator<Query>
{
    public Validator()
    {
        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Category ID is required.");
    }
}
