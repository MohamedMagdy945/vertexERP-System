namespace VertexERP.Domain.Module.Inventory.Enums;

public enum StockMovementType
{
    None = 0,

    OpeningBalance = 1,

    Purchase = 2,          // Receipt
    Sale = 3,              // Issue

    CustomerReturn = 4,    // Receipt
    SupplierReturn = 5,    // Issue

    Transfer = 6,          // TransferIn + TransferOut

    Adjustment = 7,

    Production = 8,        // Receipt (Finished Product)
    Consumption = 9        // Issue (Raw Material)
}