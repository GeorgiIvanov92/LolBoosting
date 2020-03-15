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
        private readonly RIotService _riotService;

        public SummonerController(RIotService riotService)
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
    }
}