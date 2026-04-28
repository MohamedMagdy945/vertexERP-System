using CorrelationId.DependencyInjection;

namespace VertexERP.API.Extensions
{
    public static class CorrelationExtention
    {
        public static IServiceCollection AddCorrelationIdSetup(this IServiceCollection services)
        {
            services.AddDefaultCorrelationId(options =>
            {
                options.AddToLoggingScope = true;
                options.UpdateTraceIdentifier = true;
                options.RequestHeader = "X-Correlation-Id";
                options.ResponseHeader = "X-Correlation-Id";
                options.IncludeInResponse = true;
            });

            return services;
        }
    }
}
