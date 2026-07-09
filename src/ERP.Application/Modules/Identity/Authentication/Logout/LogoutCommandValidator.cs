using FluentValidation;

namespace VertexERP.Application.Modules.Identity.Authentication.Logout;

public class LogoutCommandValidator
    : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {

        RuleFor(x => x.RefreshToken)
           .NotEmpty()
           .WithMessage("Refresh token is required.");

    }
}


