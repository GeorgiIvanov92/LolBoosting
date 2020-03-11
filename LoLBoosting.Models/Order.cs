using LolBoosting.Contracts.Orders;
using LoLBoosting.Contracts;
using System.Collections.Generic;

namespace LolBoosting.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
        //public EOrderType OrderType { get; set; }
        //public decimal BoosterEarningsPerWin { get; set; }
        //public decimal BoosterEarningsPerGame { get; set; }
        //public decimal BoosterEarningsPer20LP { get; set; }
        //public ETier StartingTier { get; set; }
        //public EDivision StartingDivision { get; set; }
        //public int StartingLP { get; set; }
        //public ETier CurrentTier { get; set; }
        //public EDivision CurrentDivision { get; set; }
        //public int CurrentLP { get; set; }
        //public ETier DesiredTier { get; set; }
        //public EDivision DesiredDivision { get; set; }
        //public int DesiredLP { get; set; }
        //public int PurchasedGames { get; set; }
        //public int RemainingGames { get; set; }
        public string CustomerId { get; set; }
        public virtual User Customer { get; set; }

        public string? BoosterId { get; set; }
        public virtual User Booster { get; set; }

        public EOrderStatus OrderStatus { get; set; }
    }
}
