using FluentValidation;

namespace VertexERP.Application.Modules.Identity.Roles.Create;

public sealed class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Role name is required.")
            .MaximumLength(100)
            .WithMessage("Role name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.");

        RuleForEach(x => x.Permissions)
            .MaximumLength(100)
            .WithMessage("Permission must not exceed 100 characters.");
    }
}
