using LoLBoosting.Common.Dtos;
using LoLBoosting.Contracts.Orders;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoLBoosting.WebApi.Communication.Http
{
    public class RiotApiClient
    {
        private const string BaseUrl = "https://{0}.api.riotgames.com";
        public HttpClient Client { get; }

        public RiotApiClient(HttpClient client)
        {
            //Change Defaults
            client.BaseAddress = new Uri(string.Format(BaseUrl, "euw1"));
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept",
                "application/x-www-form-urlencoded; charset=UTF-8");
            client.DefaultRequestHeaders.Add("Accept-Language",
                "en-GB,en-US;q=0.9,en;q=0.8,bg;q=0.7");
            client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36");
            client.DefaultRequestHeaders.Add("X-Riot-Token",
                "RGAPI-98b6144c-49a2-4878-9981-c5a708ff2051"); //move to constants or settings

            Client = client;
        }

        public async Task<Summoner> GetSummonerDetailsAsync(string summonerName, EServer summonerServer)
        {
            Client.BaseAddress = new Uri(string.Format(BaseUrl, summonerServer.ToString()));
            var response = await Client.GetAsync($"/lol/summoner/v4/summoners/by-name/{summonerName}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync
                <Summoner>(responseStream);
        }
    }
}
