using LolBoosting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LolBoosting.Controllers.OrderController;

namespace LolBoosting.Extensions
{
    public static class OrderInExtensions
    {
        public static Order ToOrder(this OrderIn orderIn)
        {
            return new Order
            {
                AccountUsername = orderIn.AccountUsername,
                AccountPassword = orderIn.AccountPassword,
            };
        }
    }
}
