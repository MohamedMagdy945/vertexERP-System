using FluentValidation;

namespace VertexERP.Application.Modules.Inventory.Stocks.Commands.DeleteStock;

public class DeleteStockCommandValidator : AbstractValidator<DeleteStockCommand>
{
    public DeleteStockCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.WarehouseId)
            .GreaterThan(0).WithMessage("Warehouse ID must be greater than 0.");

    }
}