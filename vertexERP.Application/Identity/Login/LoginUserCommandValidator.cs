using FluentValidation;

namespace VertexERP.Application.Identity.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {

            RuleFor(v => v.Username).NotEmpty().MaximumLength(50);

            RuleFor(v => v.Password)
                .NotEmpty()
                .MinimumLength(6);

        }
    }
}
