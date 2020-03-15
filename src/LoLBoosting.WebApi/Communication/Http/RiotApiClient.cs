using LoLBoosting.Contracts.Dtos;
using LoLBoosting.Contracts.Orders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            var response = await Client.GetAsync($"api/Summoner/GetSummoner?summonerName={summonerName}&serverName={summonerServer}");

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            var summoner = JsonConvert.DeserializeObject<Summoner>(responseStream);

            return summoner;
        }

        public async Task<IEnumerable<League>> GetLeagueDetailsAsync(string summonerName, EServer summonerServer)
        {
            var response = await Client.GetAsync($"api/Summoner/GetLeague?summonerName={summonerName}&serverName={summonerServer}");

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            var leagues = JsonConvert.DeserializeObject<IEnumerable<League>>(responseStream);

            return leagues;
        }
    }
}
