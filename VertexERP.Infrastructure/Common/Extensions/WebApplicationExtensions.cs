using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;

namespace VertexERP.Infrastructure.Common.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var seeders = scope.ServiceProvider
            .GetServices<IDataSeeder>()
            .OrderBy(seeder => seeder.Order);

        foreach (var seeder in seeders)
            await seeder.SeedAsync();
    }
}