using LoLBoosting.Data.Context;
using LoLBoosting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoLBoosting.Contracts.Models;

namespace LoLBoosting.Data.Seeding
{
    class TierRateSeeder : ISeeder
    {
        private Dictionary<int, TierRate> _tierRates = new Dictionary<int, TierRate>
        {
            {1 , new TierRate{TierRateId = ETier.Iron, Price = 1.8, TierName = "Iron"} },
            {2 , new TierRate{TierRateId = ETier.Bronze, Price = 2.2, TierName = "Bronze"} },
            {3 , new TierRate{TierRateId = ETier.Silver, Price = 3.4, TierName = "Silver"} },
            {4 , new TierRate{TierRateId = ETier.Gold, Price = 4.8, TierName = "Gold"} },
            {5 , new TierRate{TierRateId = ETier.Platinum, Price = 8, TierName = "Platinum"} },
            {6 , new TierRate{TierRateId = ETier.Diamond, Price = 13.5, TierName = "Diamond"} },
        };
        public async Task SeedAsync(LolBoostingDbContext dbContext, IServiceProvider serviceProvider)
        {
            var tierRatesToAdd = _tierRates.Where(z => dbContext.TierRates.Find(z.Value.TierRateId) == null).ToDictionary(z => z.Key, z => z.Value);

            dbContext.AddRange(tierRatesToAdd.Values);

            await dbContext.SaveChangesAsync();
        }
    }
}
