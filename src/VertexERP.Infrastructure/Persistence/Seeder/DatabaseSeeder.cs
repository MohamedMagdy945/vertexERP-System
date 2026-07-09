namespace VertexERP.Infrastructure.Persistence.Seeder
{
    public class DatabaseSeeder
    {
        private readonly PermissionSeeder _permissionSeeder;
        private readonly UserSeeder _userSeeder;

        public DatabaseSeeder(
            PermissionSeeder permissionSeeder,
            UserSeeder userSeeder)
        {
            _permissionSeeder = permissionSeeder;
            _userSeeder = userSeeder;
        }

        public async Task SeedAsync()
        {
            await _permissionSeeder.SeedAsync();
            await _userSeeder.SeedAsync();
        }
    }
}
