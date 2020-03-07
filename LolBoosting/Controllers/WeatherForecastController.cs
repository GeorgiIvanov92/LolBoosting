using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LolBoosting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LolBoosting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private RoleManager<IdentityRole> _roleManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var a = User.IsInRole("Client");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string role)
        {
           var roleTask = await _roleManager.CreateAsync(new IdentityRole(role));

           if (roleTask.Succeeded)
           {
               return Ok();
           }

           return BadRequest();
        }
    }
}
