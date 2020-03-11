using LolBoosting.Data.Context;
using System;
using System.Threading.Tasks;

namespace LoLBoosting.Data.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(LolBoostingDbContext dbContext, IServiceProvider serviceProvider);
    }
}
