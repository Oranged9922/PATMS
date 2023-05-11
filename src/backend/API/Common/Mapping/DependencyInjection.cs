using Mapster;
using MapsterMapper;
using System.Reflection;

namespace API.Common.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            TypeAdapterConfig cfg = TypeAdapterConfig.GlobalSettings;
            _ = cfg.Scan(Assembly.GetExecutingAssembly());

            _ = services.AddSingleton(cfg);
            _ = services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}
