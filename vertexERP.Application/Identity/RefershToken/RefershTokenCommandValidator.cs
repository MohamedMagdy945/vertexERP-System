using FluentValidation;

namespace VertexERP.Application.Identity.RefershToken
{
    public class LoginUserCommandValidator : AbstractValidator<RefershTokenCommand>
    {
        public LoginUserCommandValidator()
        {

            RuleFor(v => v.Username).
                NotEmpty().MaximumLength(50);

            RuleFor(v => v.RefershToken)
                .NotEmpty()
                .MinimumLength(6);


        }
    }
}
