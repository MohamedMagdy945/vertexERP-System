using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Modules.Catalog.Products.Commands.Create;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Product code is required.")
            .MaximumLength(40).WithMessage("Product code must not exceed 50 characters.")
            .Matches(@"^[a-zA-Z0-9-_]+$").WithMessage("Code can only contain alphanumeric characters, hyphens, and underscores.");

        RuleFor(x => x.CostPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Cost price must be greater than or equal to 0.");


        RuleFor(x => x.SellingPrice)
            .GreaterThan(0).WithMessage("Selling price must be greater than 0.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.UnitId)
            .NotEmpty().WithMessage("Measurement unit ID is required.");

        When(x => !string.IsNullOrWhiteSpace(x.Barcode), () =>
        {
            RuleFor(x => x.Barcode)
                .MaximumLength(40).WithMessage("Barcode must not exceed 40 characters.");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
        {
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
        });

        RuleFor(x => x.Images)
        .Must(images => images == null || images.Count <= 6)
        .WithMessage("You can upload maximum 6 images.");

        When(x => x.Images is { Count: > 0 }, () =>
        {
            RuleForEach(x => x.Images)
                .Must(HaveValidSize)
                .WithMessage("Each image size must be less than 5 MB.")
                .Must(HaveValidExtension)
                .WithMessage("Only JPG, JPEG, and PNG images are allowed.");
        });
    }
    private static bool HaveValidSize(IFormFile file)
    => file is { Length: > 0 and <= 6 * 1024 * 1024 };

    private static bool HaveValidExtension(IFormFile file)
    {
        if (file is null) return false;
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        return ext is ".jpg" or ".jpeg" or ".png" or ".webp";
    }
}
