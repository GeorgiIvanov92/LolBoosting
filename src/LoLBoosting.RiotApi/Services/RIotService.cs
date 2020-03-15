using LoLBoosting.Constants;
using LoLBoosting.Contracts.Dtos;
using LoLBoosting.Contracts.Orders;
using LoLBoosting.RiotApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoLBoosting.RiotApi.Services
{
    public class RiotService
    {
        private readonly IOptionsMonitor<DevelopmentApiKey> _optionsMonitor;

        public HttpClient Client { get; }

        public RiotService(HttpClient client, IOptionsMonitor<DevelopmentApiKey> optionsMonitor)
        {
            client.BaseAddress = new Uri(string.Format(RiotApiConstants.BaseUrl, "euw1"));
            client.DefaultRequestHeaders.Add("Origin",
                "https://developer.riotgames.com");
            client.DefaultRequestHeaders.Add("Accept-Charset",
                "UTF-8");
            client.DefaultRequestHeaders.Add("Accept-Language",
                "en-GB,en-US;q=0.9,en;q=0.8,bg;q=0.7");
            client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36");

            client.DefaultRequestHeaders.Add(RiotApiConstants.ApiKeyHeader,
                $"{optionsMonitor.CurrentValue.RiotToken}");

            Client = client;
            _optionsMonitor = optionsMonitor;

            _optionsMonitor.OnChange(c =>
            {
                Client.DefaultRequestHeaders.Remove(RiotApiConstants.ApiKeyHeader);
                Client.DefaultRequestHeaders.Add(RiotApiConstants.ApiKeyHeader, $"{c.RiotToken}");
            });
        }

        public async Task<Summoner> GetSummonerDetailsAsync(string summonerName, EServer summonerServer)
        {
            Client.BaseAddress = new Uri(string.Format(RiotApiConstants.BaseUrl, summonerServer.ToString()));
            var response = await Client.GetAsync(string.Format(RiotApiConstants.SummonerByName, summonerName));

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            var summoner = JsonConvert.DeserializeObject<Summoner>(responseStream);

            return summoner;
        }

        public async Task<IEnumerable<League>> GetLeagueDetailsAsync(string encryptedSummonerId, EServer summonerServer)
        {
            Client.BaseAddress = new Uri(string.Format(RiotApiConstants.BaseUrl, summonerServer.ToString()));
            var response = await Client.GetAsync(string.Format(RiotApiConstants.LeagueBySummonerId, encryptedSummonerId));

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            var leagues = JsonConvert.DeserializeObject<IEnumerable<League>>(responseStream);

            return leagues;
        }
    }
}
