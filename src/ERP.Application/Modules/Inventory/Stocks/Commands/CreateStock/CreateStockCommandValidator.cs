using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Stocks.Commands.CreateStock;

public class CreateStockCommandValidator : AbstractValidator<CreateStockCommand>
{
    public CreateStockCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.WarehouseId)
            .GreaterThan(0).WithMessage("Warehouse ID must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");
    }
}