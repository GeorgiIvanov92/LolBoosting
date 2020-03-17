using System.Collections.Generic;
using System.Threading.Tasks;
using LoLBoosting.Constants;
using LoLBoosting.Contracts.Dtos;
using LoLBoosting.Contracts.Orders;
using LoLBoosting.RiotApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoLBoosting.RiotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerController : ControllerBase
    {
        private readonly RiotService _riotService;

        public SummonerController(RiotService riotService)
        {
            _riotService = riotService;
        }

        [HttpGet]
        [Route(GlobalConstants.RiotApiEndpoints.SummonerDetails)]
        public async Task<Summoner> GetSummonerDetails(string summonerName, EServer serverName)
        {
            return await _riotService.GetSummonerDetailsAsync(summonerName, serverName);
        }

        [HttpPost]
        [Route(GlobalConstants.RiotApiEndpoints.LeaguesOfSummoner)]
        public async Task<IEnumerable<League>> GetLegueDetailsOfSummoner([FromBody] Summoner summoner)
        {
            return await _riotService.GetLeagueDetailsAsync(summoner.Id, summoner.Server);
        }
    }
}