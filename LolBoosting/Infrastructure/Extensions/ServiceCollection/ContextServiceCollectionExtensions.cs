using LolBoosting.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static LoLBoosting.Constants.ApiConstants;

namespace LolBoostingWebApi.Infrastructure.Extensions.ServiceCollection
{
    public static class ContextServiceCollectionExtensions
    {
        public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LolBoostingDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ConfigurtaionConfigKeys.SqlConnectonNameKey)));

            return services;
        }
    }
}
