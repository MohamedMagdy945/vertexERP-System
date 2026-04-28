namespace VertexERP.API.Configurations.Versioning
{
    public static class VersioningConfiguration
    {
        public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
           .AddMvc()
           .AddApiExplorer(options =>
           {
               options.GroupNameFormat = "'v'VVV";
               options.SubstituteApiVersionInUrl = true;
           });
            return services;
        }
    }
}
