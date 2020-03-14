using LoLBoosting.Data.Context;
using LoLBoosting.Entities;

namespace LoLBoosting.Data.Repository
{
    public class OrderRepository : DeletableEntityRepository<Order>
    {
        public OrderRepository(LolBoostingDbContext context) : base(context)
        {

        }
    }
}
