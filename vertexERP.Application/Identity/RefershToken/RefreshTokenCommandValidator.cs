using FluentValidation;

namespace VertexERP.Application.Identity.RefershToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {

            RuleFor(v => v.Username).
                NotEmpty().MaximumLength(50);

            RuleFor(v => v.RefreshToken)
                .NotEmpty()
                .MinimumLength(6);


        }
    }
}
