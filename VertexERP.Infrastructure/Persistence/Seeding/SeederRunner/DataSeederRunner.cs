namespace VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;

public class DataSeederRunner(IEnumerable<IDataSeeder> seeders)
{
    public async Task SeedAsync()
    {
        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        }

    }
}