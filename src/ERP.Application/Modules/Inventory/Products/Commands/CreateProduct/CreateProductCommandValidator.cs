using FluentValidation;
using Microsoft.AspNetCore.Http;
using VertexERP.Application.Abstractions.Persistence;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(150).WithMessage("Product name must not exceed 150 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Product code is required.")
            .MaximumLength(50).WithMessage("Product code must not exceed 50 characters.");

        RuleFor(x => x.CostPrice)
            .GreaterThan(0).WithMessage("Cost price must be greater than 0.");

        RuleFor(x => x.SellingPrice)
             .GreaterThan(0).WithMessage("Selling price must be greater than 0.");


        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("A valid category must be selected.");

        RuleFor(x => x.Unit)
            .IsInEnum().WithMessage("The selected unit type is invalid.");

        When(x => x.Image != null, () =>
        {
            RuleFor(x => x.Image!)
                .Must(HaveValidSize).WithMessage("Image size must be less than 5 MB.")
                .Must(HaveValidExtension).WithMessage("Only JPG, JPEG, and PNG images are allowed.");
        });
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