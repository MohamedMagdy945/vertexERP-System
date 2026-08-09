using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Modules.Catalog.Categories.Create;

public sealed class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        When(x => x.Image is not null, () =>
        {
            RuleFor(x => x.Image!)
                .Must(HaveValidSize)
                .WithMessage("Image size must be less than 6 MB.")
                .Must(HaveValidExtension)
                .WithMessage("Only JPG, JPEG, PNG, and WEBP images are allowed.");
        });
    }

    private static bool HaveValidSize(IFormFile file)
        => file is { Length: > 0 and <= 6 * 1024 * 1024 };

    private static bool HaveValidExtension(IFormFile file)
    {
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        return ext is ".jpg" or ".jpeg" or ".png" or ".webp";
    }
}