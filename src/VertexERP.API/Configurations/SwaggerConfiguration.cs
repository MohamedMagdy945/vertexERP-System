using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace VertexERP.API.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Vertex ERP API",
                Version = "v1",
                Description = "Vertex ERP REST API",
                Contact = new OpenApiContact
                {
                    Name = "Mohamed Magdy",
                    Email = "mohamedmagdy000022@gmail.com"
                }
            });
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "Vertex ERP API",
                Version = "v2",
                Description = "Vertex ERP REST API",
                Contact = new OpenApiContact
                {
                    Name = "Mohamed Magdy",
                    Email = "mohamedmagdy000022@gmail.com"
                }
            });

            const string scheme = "Bearer";

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(scheme, document)] = []
            });
        });

        return services;
    }

    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return app;

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vertex ERP API v1");
            options.RoutePrefix = string.Empty;
            options.DisplayRequestDuration();
            options.EnablePersistAuthorization();
            options.DocExpansion(DocExpansion.None);
            options.DefaultModelsExpandDepth(-1);
            options.EnableDeepLinking();
        });

        return app;
    }
}