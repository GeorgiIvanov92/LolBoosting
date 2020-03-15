using Newtonsoft.Json;

namespace LoLBoosting.Contracts.Dtos
{
    public class League
    {
        [JsonProperty("queueType")]
        public string QueueType  { get; set; }

        [JsonProperty("summonerName")]
        public string SummonerName { get; set; }

        [JsonProperty("hotStreak")]
        public bool HotStreak { get; set; }

        [JsonProperty("wins")]
        public int Wins { get; set; }

        [JsonProperty("losses")]
        public int Losses { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("tier")]
        public string Tier { get; set; }

        [JsonProperty("inactive")]
        public bool Inactive { get; set; }

        [JsonProperty("leagueId")]
        public string LeagueId { get; set; }

        [JsonProperty("summonerId")]
        public string SummonerId { get; set; }

        [JsonProperty("leaguePoints")]
        public int LeaguePoints { get; set; }
    }
}
