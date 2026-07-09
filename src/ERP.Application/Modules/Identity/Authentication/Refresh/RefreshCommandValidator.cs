using FluentValidation;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public class RefreshCommandValidator
    : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required.");

    }
}


