using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.AddProductImage;


public class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
{

    public AddProductImageCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.Image)
              .Must(HaveValidSize).WithMessage("Image size must be less than 5 MB.")
              .Must(HaveValidExtension).WithMessage("Only JPG, JPEG, and PNG images are allowed.");
    }

    private bool HaveValidSize(IFormFile file)
    {
        const int maxBytes = 5 * 1024 * 1024;
        return file.Length <= maxBytes;
    }

    private bool HaveValidExtension(IFormFile file)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }
}