namespace VertexERP.Domain.Module.Inventory.Enums;

public enum InventoryTransactionType
{
    OpeningBalance = 1,
    Receipt = 2,
    Issue = 3,
    TransferIn = 4,
    TransferOut = 5,
    Adjustment = 6
}