using LoLBoosting.Contracts.Orders;

namespace LoLBoosting.Common.Dtos
{
    public class Summoner
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AccountId { get; set; }
        public string Puuid { get; set; }
        public int SummonerLevel { get; set; }
    }
}
