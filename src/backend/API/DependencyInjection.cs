using API.Common.Errors;
using API.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            _ = services.AddControllers();
            _ = services.AddSingleton<ProblemDetailsFactory, PatmsProblemDetailsFactory>();
            _ = services.AddMappings();
            return services;
        }
    }
}
