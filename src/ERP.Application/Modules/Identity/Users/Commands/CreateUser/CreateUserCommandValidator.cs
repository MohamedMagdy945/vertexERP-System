using FluentValidation;

namespace VertexERP.Application.Modules.Identity.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);


        RuleFor(x => x.Password)
           .NotEmpty()
           .MinimumLength(8)
           .MaximumLength(128)
           .WithMessage("Password must be between 8 and 128 characters.")
           .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>/?]).+$")
           .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Password and confirmation password do not match.");

        RuleFor(x => x.PermissionIds)
            .NotNull();

        RuleForEach(x => x.PermissionIds)
            .GreaterThan(0);

        RuleFor(x => x.IsEnabled)
            .NotNull();
    }
}