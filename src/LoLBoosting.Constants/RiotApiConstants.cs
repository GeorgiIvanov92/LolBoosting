namespace LoLBoosting.Constants
{
    public class RiotApiConstants
    {
        public const string ApiKeyFileName = "developmentapikey.json";

        public const string ApiKeyClassName = "DevelopmentApiKey";

        public const string BaseUrl = "https://{0}.api.riotgames.com";  

        public const string ApiKeyHeader = "X-Riot-Token";  

        public const string SummonerByName = "/lol/summoner/v4/summoners/by-name/{0}";  

        public const string LeagueBySummonerId = "/lol/league/v4/entries/by-summoner/{0}";  
    }
}
