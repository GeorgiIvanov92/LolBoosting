using System.Collections.Generic;
using System.Threading.Tasks;
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
        [Route("GetSummoner")]
        public async Task<Summoner> GetSummonerDetails(string summonerName, EServer serverName)
        {
            var result = await _riotService.GetSummonerDetailsAsync(summonerName, serverName);
            return result;
        }

        [HttpGet]
        [Route("GetLeague")]
        public async Task<IEnumerable<League>> GetLegueDetails(string encriptedSummonerId, EServer serverName)
        {
            var result = await _riotService.GetLeagueDetailsAsync(encriptedSummonerId, serverName);
            return result;
        }
    }
}