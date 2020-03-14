using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLBoosting.Common.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoLBoosting.RiotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerController : ControllerBase
    {
        [HttpPost]
        public Task<bool> ValidateSummoner([FromBody] Summoner summoner)
        {

            return Task.FromResult(true);
        }
    }
}