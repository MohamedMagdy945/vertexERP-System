using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace VertexERP.API.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                // JWT
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token only"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // Important: API Versioning integration
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.ActionDescriptor.EndpointMetadata
                        .OfType<ApiVersionAttribute>()
                        .Any())
                        return true;

                    var versions = apiDesc.ActionDescriptor.EndpointMetadata
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(v => v.Versions);

                    return versions.Any(v => $"v{v}" == docName);
                });

                options.CustomSchemaIds(type => type.FullName);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "VertexERP API V1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "VertexERP API V2");

                options.RoutePrefix = "swagger";
                options.DocumentTitle = "VertexERP API Docs";

                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                options.DefaultModelsExpandDepth(-1);
                options.DisplayRequestDuration();
                options.EnableFilter();
                options.EnableDeepLinking();
            });

            return app;
        }
    }
}