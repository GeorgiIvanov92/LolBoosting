using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoLBoosting.WebApi.Communication.Http
{
    public class RiotApiClient
    {
        public HttpClient Client { get; }

        public RiotApiClient(HttpClient client)
        {
            //Change Defaults
            client.BaseAddress = new Uri("https://api.github.com/");
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept",
                "application/vnd.github.v3+json");
            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent",
                "HttpClientFactory-Sample");

            Client = client;
        }

        public async Task<bool> GetSummonerDetails(string summonerName, string serverName)
        {
            var response = await Client.GetAsync("google.com");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync
                <bool>(responseStream);
        }
    }
}
