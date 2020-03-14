using LoLBoosting.Entities;
using LoLBoosting.WebApi.Controllers;

namespace LoLBoosting.WebApi.Extensions
{
    public static class OrderInExtensions
    {
        public static Order ToOrder(this OrderController.OrderIn orderIn)
        {
            return new Order
            {
                AccountUsername = orderIn.AccountUsername,
                AccountPassword = orderIn.AccountPassword,
            };
        }
    }
}
