using LoLBoosting.Constants;
using LoLBoosting.Contracts.Dtos;
using LoLBoosting.Contracts.Orders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoLBoosting.WebApi.Communication.Http
{
    public class RiotApiClient
    {
        private const string BaseUrl = "https://localhost:44380";
        public HttpClient Client { get; }

        public RiotApiClient(HttpClient client)
        {
            client.BaseAddress = new Uri(BaseUrl);
            Client = client;
        }

        public async Task<Summoner> GetSummonerDetailsAsync(string summonerName, EServer summonerServer)
        {
            var url = string.Format(GlobalConstants.RiotApiEndpoints.DefaultRoute, GlobalConstants.RiotApiEndpoints.SummonerDetails) + $"?summonerName={summonerName}&serverName={summonerServer}";
            var response = await Client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            var summoner = JsonConvert.DeserializeObject<Summoner>(responseStream);

            return summoner;
        }

        public async Task<IEnumerable<League>> GetLeagueDetailsAsync(Summoner summoner)
        {

            var url = string.Format(GlobalConstants.RiotApiEndpoints.DefaultRoute, GlobalConstants.RiotApiEndpoints.LeaguesOfSummoner);
            var contentAsSting = JsonConvert.SerializeObject(summoner);
            var content = new StringContent(contentAsSting, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            var leagues = JsonConvert.DeserializeObject<IEnumerable<League>>(responseStream);

            return leagues;
        }
    }
}
