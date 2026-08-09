namespace VertexERP.Infrastructure.Persistence.Seeding.SeederRunner
{
    public interface IDataSeeder
    {
        public int Order => 1;
        Task SeedAsync();
    }
}
