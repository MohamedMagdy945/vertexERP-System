using FluentValidation;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}

