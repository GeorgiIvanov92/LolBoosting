using LolBoosting.Data.Context;
using LolBoosting.Models;

namespace LoLBoosting.Data.Repository
{
    public class UserRepository : DeletableEntityRepository<User>
    {
        public UserRepository(LolBoostingDbContext context) : base(context)
        {

        }
    }
}
