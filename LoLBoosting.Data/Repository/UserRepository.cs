﻿using LolBoosting.Data.Context;
using LolBoosting.Models;

namespace LoLBoosting.Data.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(LolBoostingDbContext context) : base(context)
        {

        }
    }
}
