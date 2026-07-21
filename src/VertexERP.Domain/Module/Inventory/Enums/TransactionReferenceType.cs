namespace VertexERP.Domain.Module.Inventory.Enums;

public enum TransactionReferenceType
{
    None = 0,
    OpeningBalance = 1,
    Purchase = 2,
    Sale = 3,
    CustomerReturn = 4,
    SupplierReturn = 5,
    Transfer = 6,
    Adjustment = 7,
    Production = 8,
    Consumption = 9
}