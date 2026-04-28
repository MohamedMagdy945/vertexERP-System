namespace VertexERP.Infrastructure.Persistence.SeederRunner
{
    public class DataSeederRunner
    {
        private readonly IEnumerable<IDataSeeder> _seeders;

        public DataSeederRunner(IEnumerable<IDataSeeder> seeders)
        {
            _seeders = seeders;
        }

        public async Task SeedAsync()
        {
            foreach (var seeder in _seeders)
            {
                await seeder.SeedAsync();
            }
        }
    }
}
