using System.Net;
using System.Threading.Tasks;
using LolBoosting.Data.Context;
using LolBoosting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(typeof(OrderOut), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderIn orderIn)
        {
            if (ModelState.IsValid)
            {
                //TODO: create mapping orderId -> order
                var order = new Order();

                //TODO: change to repository 
                _boostingDbContext.Orders.Add(order);
                await _boostingDbContext.SaveChangesAsync();

                //TODO: create mapping order -> orderOut
                var orderOut = new OrderOut();

                return Created($"{ControllerContext.ActionDescriptor.ControllerName}/{order.OrderId}", orderOut);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("{orderId}")]
        [Authorize(Roles = "Client,Administrator")]
        [ProducesResponseType(typeof(OrderOut), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = _boostingDbContext.Orders.Find(id);

            //TODO: create mapping order -> orderOut
            var orderOut = new OrderOut();

            return Ok(orderOut);
        }

        public class OrderIn
        {
            public User Client { get; set; }
            public Order Order { get; set; }
        }

        public class OrderOut 
        {
            public User Client { get; set; }
            public Order Order { get; set; }
        }
    }
}
