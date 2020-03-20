using AutoMapper;
using LoLBoosting.Entities;
using LoLBoosting.Services.Mapping;

namespace LoLBoosting.WebApi.ViewModels.Orders
{
    public class OrderOut : IMapFrom<Order>
    {
        public string ClientId { get; set; }
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
    }
}
