using Asp.Versioning;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VertexERP.API.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Basket API",
                        Description = "An ASP.NET Core Web API for managing basket v1 micro-services in commerce application",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Mohamed Magdy",
                            Email = "mohamedmagdy000022@gmail.com",
                        }
                    });
                options.SwaggerDoc("v2",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = "v2",
                        Title = "Basket API",
                        Description = "An ASP.NET Core Web API for managing basket v2 micro-services in commerce application",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Mohamed Magdy",
                            Email = "mohamedmagdy000022@gmail.com",
                        }
                    });
                options.DocInclusionPredicate((version, apiDescription) =>
                {
                    if (!apiDescription.TryGetMethodInfo(out var methodInfo))
                    {
                        return false;
                    }
                    var versions = methodInfo.DeclaringType?
                                    .GetCustomAttributes(true)
                                    .OfType<ApiVersionAttribute>()
                                    .SelectMany(attr => attr.Versions);
                    return versions?.Any(v => $"v{v.ToString()}" == version) ?? false;
                }

                );

                options.DocInclusionPredicate((version, apiDescription) =>
                {
                    if (!apiDescription.TryGetMethodInfo(out var methodInfo))
                    {
                        return false;
                    }
                    var versions = methodInfo.DeclaringType?
                                    .GetCustomAttributes(true)
                                    .OfType<ApiVersionAttribute>()
                                    .SelectMany(attr => attr.Versions);
                    return versions?.Any(v => $"v{v.ToString()}" == version) ?? false;
                }

                );

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

            });

            return app;
        }
    }
}