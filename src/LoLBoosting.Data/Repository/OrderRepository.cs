using LoLBoosting.Data.Context;
using LoLBoosting.Models;

namespace LoLBoosting.Data.Repository
{
    public class OrderRepository : DeletableEntityRepository<Order>
    {
        public OrderRepository(LolBoostingDbContext context) : base(context)
        {

        }
    }
}
