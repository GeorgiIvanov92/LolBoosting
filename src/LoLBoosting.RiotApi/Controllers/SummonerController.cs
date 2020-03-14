using System.Threading.Tasks;
using LoLBoosting.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LoLBoosting.RiotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerController : ControllerBase
    {
        [HttpPost]
        public Task<Summoner> GetSummonerDetails(string summonerName)
        {
            var result = new Summoner();

            return Task.FromResult(result);
        }
    }
}