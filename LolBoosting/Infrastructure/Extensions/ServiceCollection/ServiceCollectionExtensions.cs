using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LolBoosting.Infrastructure.Extensions.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            

            return services;
        }
    }
}
