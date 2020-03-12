using System.Net;
using System.Threading.Tasks;
using LoLBoosting.Data.Repository;
using LolBoosting.Extensions;
using LolBoosting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LolBoosting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private IRepository<Order> _orderRepository;
        private UserManager<User> _userManager;

        public OrderController(IRepository<Order> orderRepositpory, UserManager<User> userManager)
        {
            _orderRepository = orderRepositpory;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "Client,Administrator")]
        [ProducesResponseType(typeof(OrderOut), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderIn orderIn)
        {
            if (ModelState.IsValid)
            {
                var order = orderIn.ToOrder();
                var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
                order.CustomerId = user.Id;
                order.OrderStatus = Contracts.Orders.EOrderStatus.WaitingVerification;

                _orderRepository.Add(order);
                await _orderRepository.SaveChangesAsync();

                var orderOut = new OrderOut
                {
                    ClientId = order.Customer.Id,
                    AccountUsername = order.AccountUsername,
                    AccountPassword = order.AccountPassword
                };

                return Created($"{ControllerContext.ActionDescriptor.ControllerName}/{order.OrderId}", orderOut);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Client,Administrator")]
        [ProducesResponseType(typeof(OrderOut), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = _orderRepository.Find(id);

            //TODO: create mapping order -> orderOut
            var orderOut = new OrderOut();

            return Ok(orderOut);
        }

        public class OrderIn
        {
            public string AccountUsername { get; set; }
            public string AccountPassword { get; set; }
        }

        public class OrderOut 
        {
            public string ClientId { get; set; }
            public string AccountUsername { get; set; }
            public string AccountPassword { get; set; }
        }
    }
}
