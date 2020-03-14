using System.Net;
using System.Threading.Tasks;
using LoLBoosting.Data.Repository;
using LolBoosting.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LolBoosting.Contracts.Orders;
using LolBoosting.Models;

namespace LolBoostingWebApi.Controllers
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderIn"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
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
                order.OrderStatus = EOrderStatus.WaitingVerification;

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boosterName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Assign")]
        [Authorize(Roles = "Booster,Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AssignOrder(int id, string? boosterName)
        {
            var order = _orderRepository.Find(id);
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);

            if (order.OrderStatus.Equals(EOrderStatus.WaitingForBooster))
            {
                if (User.IsInRole("Administrator")
                    && !string.IsNullOrWhiteSpace(boosterName))
                {
                    user = await _userManager.FindByNameAsync(boosterName);
                }

                order.BoosterId = user.Id;
                order.OrderStatus = EOrderStatus.ClaimedByBooster;

                _orderRepository.Update(order);
                await _orderRepository.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Sorry. Order cannot be assigned.");
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
