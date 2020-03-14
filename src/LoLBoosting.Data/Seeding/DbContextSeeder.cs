using LoLBoosting.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoLBoosting.Data.Seeding
{
    public class DbContextSeeder : ISeeder
    {
        public async Task SeedAsync(LolBoostingDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(LolBoostingDbContext));

            var seeders = new List<ISeeder>
                          {
                              new RolesSeeder(),
                              new UsersSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}
