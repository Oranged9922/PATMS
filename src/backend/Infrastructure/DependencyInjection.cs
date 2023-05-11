using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {

            // Add db context
            services.AddAuth(configuration);
            services.AddPersistance();
            // add datetimeprovider as singleton

            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            // add repos (scoped)
            return services;
        }

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            // add auth (jwt most likely)
            return services;
        }
    }
}
