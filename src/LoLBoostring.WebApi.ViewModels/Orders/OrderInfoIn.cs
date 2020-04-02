using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LoLBoosting.Contracts.Models;
using LoLBoosting.Contracts.Orders;

namespace LoLBoosting.WebApi.ViewModels.Orders
{
    public class OrderInfoIn
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        public EServer Server { get; set; }
        [Required]
        public EOrderType OrderType { get; set; }
        public int NumberOfGames { get; set; }
        public ETier DesiredTier { get; set; }
        public EDivision DesiredDivision { get; set; }
    }
}
