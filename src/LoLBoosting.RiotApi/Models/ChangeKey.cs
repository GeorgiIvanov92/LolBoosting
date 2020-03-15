using LoLBoosting.Constants;
using Newtonsoft.Json;

namespace LoLBoosting.RiotApi.Models
{
    public class ChangeKey
    {
        [JsonProperty(RiotApiConstants.ApiKeyClassName)]
        public DevelopmentApiKey DevelopmentApiKey { get; set; }
    }

    public class DevelopmentApiKey
    {
        public string RiotToken { get; set; }
    }
}