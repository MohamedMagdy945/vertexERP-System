using VertexERP.Infrastructure.Persistence.Seeder.Identity;
using VertexERP.Infrastructure.Persistence.Seeder.Inventory;

namespace VertexERP.Infrastructure.Persistence.Seeder;

public class DatabaseSeeder
{
    private readonly PermissionSeeder _permissionSeeder;
    private readonly UserSeeder _userSeeder;

    private readonly CategorySeeder _categorySeeder;
    private readonly ProductSeeder _productSeeder;
    private readonly WarehouseSeeder _warehouseSeeder;
    private readonly StockSeeder _stockSeeder;
    private readonly InventoryTransactionSeeder _inventoryTransactionSeeder;

    public DatabaseSeeder(
        PermissionSeeder permissionSeeder,
        UserSeeder userSeeder,
        CategorySeeder categorySeeder,
        ProductSeeder productSeeder,
        WarehouseSeeder warehouseSeeder,
        StockSeeder stockSeeder,
        InventoryTransactionSeeder inventoryTransactionSeeder)
    {
        _permissionSeeder = permissionSeeder;
        _userSeeder = userSeeder;
        _categorySeeder = categorySeeder;
        _productSeeder = productSeeder;
        _warehouseSeeder = warehouseSeeder;
        _stockSeeder = stockSeeder;
        _inventoryTransactionSeeder = inventoryTransactionSeeder;
    }

    public async Task SeedAsync()
    {
        await _permissionSeeder.SeedAsync();
        await _userSeeder.SeedAsync();

        await _categorySeeder.SeedAsync();
        await _productSeeder.SeedAsync();
        await _warehouseSeeder.SeedAsync();
        await _stockSeeder.SeedAsync();
        await _inventoryTransactionSeeder.SeedAsync();


    }
}
