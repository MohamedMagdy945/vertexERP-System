using FluentValidation;


namespace VertexERP.Application.Identity.Register
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.UserName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.Email).NotEmpty().EmailAddress();

            RuleFor(v => v.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(v => v.ConfirmPassword)
                .Equal(v => v.Password)
                .WithMessage("Passwords do not match.");
        }
    }
}


