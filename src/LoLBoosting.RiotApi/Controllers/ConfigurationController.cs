using System.Net;
using LoLBoosting.Constants;
using LoLBoosting.RiotApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LoLBoosting.RiotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IOptionsMonitor<DevelopmentApiKey> _optionsMonitor;

        public ConfigurationController(IOptionsMonitor<DevelopmentApiKey> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }

        [HttpGet]
        [Route("GetKey")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetApiKey()
        {
            if (_optionsMonitor.CurrentValue != null)
            {
                return Ok(_optionsMonitor.CurrentValue.RiotToken);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("ChangeKey")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult ChangeApiKey(string newApiKey)
        {
            if (_optionsMonitor.CurrentValue.RiotToken != newApiKey)
            {            
                var newKey = new ChangeKey { DevelopmentApiKey = new DevelopmentApiKey { RiotToken = newApiKey }};
                var configAsString = JsonConvert.SerializeObject(newKey, Formatting.Indented);
                System.IO.File.WriteAllText($@"{RiotApiConstants.ApiKeyFileName}", configAsString);

                return Ok();
            }

            return BadRequest();
        }
    }
}