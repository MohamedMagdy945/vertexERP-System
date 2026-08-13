using FluentValidation;

namespace VertexERP.Application.Modules.Identity.Users.Update;

public sealed class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage("Name is required.")
           .MaximumLength(50)
           .WithMessage("Name must not exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email address is not valid.");

        RuleFor(x => x.PortalType)
            .IsInEnum()
            .WithMessage("Portal type is not valid.");

        RuleFor(x => x.RoleIds)
            .NotNull()
            .WithMessage("RoleIds must be provided.");

        RuleForEach(x => x.RoleIds)
            .NotEmpty()
            .WithMessage("Role ID cannot be empty.");

        RuleFor(x => x.RoleIds)
            .Must(roleIds => roleIds.Distinct().Count() == roleIds.Count)
            .WithMessage("Duplicate role IDs are not allowed.");
    }
}
