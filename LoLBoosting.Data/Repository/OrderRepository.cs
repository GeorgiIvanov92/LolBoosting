using LolBoosting.Data.Context;
using LolBoosting.Models;

namespace LoLBoosting.Data.Repository
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(LolBoostingDbContext context) : base(context)
        {

        }
    }
}
