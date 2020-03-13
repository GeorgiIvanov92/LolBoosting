using LolBoosting.Data.Context;
using LolBoosting.Models;

namespace LoLBoosting.Data.Repository
{
    public class OrderRepository : DeletableEntityRepository<Order>
    {
        public OrderRepository(LolBoostingDbContext context) : base(context)
        {

        }
    }
}
