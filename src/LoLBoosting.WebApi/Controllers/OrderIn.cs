﻿using LoLBoosting.Contracts.Orders;

namespace LoLBoosting.WebApi.Controllers
{
    public class OrderIn
    {
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
        public EServer SummonerServer { get; set; }
    }
}
