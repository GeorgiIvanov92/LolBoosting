using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LolBoosting.Models.Enums;

namespace LolBoosting.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
        public OrderType OrderType { get; set; }
        public decimal BoosterEarningsPerWin { get; set; }
        public decimal BoosterEarningsPerGame { get; set; }
        public decimal BoosterEarningsPer20LP { get; set; }
        public Tier StartingTier { get; set; }
        public Division StartingDivision { get; set; }
        public int StartingLP { get; set; }
        public Tier CurrentTier { get; set; }
        public Division CurrentDivision { get; set; }
        public int CurrentLP { get; set; }
        public Tier DesiredTier { get; set; }
        public Division DesiredDivision { get; set; }
        public int DesiredLP { get; set; }
        public int PurchasedGames { get; set; }
        public int RemainingGames { get; set; }
        public virtual ICollection<UserOrder> UserOrders { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
