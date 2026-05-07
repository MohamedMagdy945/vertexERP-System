using FluentValidation;

namespace VertexERP.Application.Identity.Logout
{
    public class LogoutUserCommandValidator : AbstractValidator<LogoutUserCommand>
    {
        public LogoutUserCommandValidator()
        {

            RuleFor(v => v.RefreshToken)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
