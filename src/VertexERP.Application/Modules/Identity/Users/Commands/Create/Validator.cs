using FluentValidation;

namespace VertexERP.Application.Modules.Identity.Users.Commands.Create;

public sealed class CreateUserCommandValidator
    : AbstractValidator<Command>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}