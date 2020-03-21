using System;
using System.Collections.Generic;
using System.Text;
using LoLBoosting.Data.Context;
using LoLBoosting.Entities;

namespace LoLBoosting.Data.Repository
{
    public class TierRateRepository : BaseRepository<TierRate>
    {
        public TierRateRepository(LolBoostingDbContext context) : base(context)
        {

        }
    }
}
