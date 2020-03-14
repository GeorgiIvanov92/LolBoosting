using LoLBoosting.Data.Context;
using LoLBoosting.Models;

namespace LoLBoosting.Data.Repository
{
    public class UserRepository : DeletableEntityRepository<User>
    {
        public UserRepository(LolBoostingDbContext context) : base(context)
        {

        }
    }
}
