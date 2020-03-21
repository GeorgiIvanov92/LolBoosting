using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LoLBoosting.Contracts.Models;

namespace LoLBoosting.Entities
{
    public class TierRate
    {
        [Key]
        public ETier TierRateId { get; set; }
        public double Price { get; set; }
        public string TierName { get; set; }
    }
}
