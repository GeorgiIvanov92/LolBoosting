using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LoLBoosting.Contracts.Models;

namespace LoLBoosting.WebApi.ViewModels.Orders
{
    public class OrderMetadataOut
    {
        [Range(0, Double.MaxValue)]
        public double Price { get; set; }
        public ETier CurrentTier { get; set; }
        public EDivision CurrentDivision { get; set; }
        [Range(0, 100)]
        public double CurrentPoints { get; set; }
    }
}
