using LolBoosting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LolBoostingWebApi.Controllers;

namespace LolBoosting.Extensions
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
