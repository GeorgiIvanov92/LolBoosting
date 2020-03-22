using LolBoosting.Services;
using LoLBoosting.WebApi.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LolBoosting.WebApi.Infrastructure.Extensions.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterContext(configuration);

            services.AddSingleton<MultiplyCalculator>();

            services.AddSingleton<UserRequestRegistry>();

            services.AddScoped<AntiRequestSpamFilter>();

            return services;
        }
    }
}
