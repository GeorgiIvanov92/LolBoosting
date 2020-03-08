using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LolBoosting.Data;
using LolBoosting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LolBoosting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private LolBoostingDbContext _boostingDbContext;
        public OrderController(LolBoostingDbContext dbContext)
        {
            _boostingDbContext = dbContext;
        }

        [HttpPost]
        [Authorize(Roles = "Client,Administrator")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                _boostingDbContext.Orders.Add(order);
                await _boostingDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Client,Administrator")]
        [Route("zdr")]
        public Order GetOrderById(int id)
        {
            return _boostingDbContext.Orders.Find(id);
        }


        public class OrderIn
        {
            public User Client { get; set; }
            public Order Order { get; set; }
        }
    }
}
