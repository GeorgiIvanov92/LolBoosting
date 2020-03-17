using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoLBoosting.Data.Repository;
using LoLBoosting.WebApi.Extensions;
using LoLBoosting.Contracts.Models;
using LoLBoosting.WebApi.Communication.Http;
using LoLBoosting.Contracts.Orders;
using LoLBoosting.Entities;

namespace LoLBoosting.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private IRepository<Order> _orderRepository;
        private UserManager<User> _userManager;
        private readonly RiotApiClient _riotApiClient;

        public OrderController(IDeletebleEntityRepository<Order> orderRepositpory, UserManager<User> userManager, RiotApiClient riotApiClient)
        {
            _orderRepository = orderRepositpory;
            _userManager = userManager;
            _riotApiClient = riotApiClient;
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
                var summoner = await _riotApiClient.GetSummonerDetailsAsync(orderIn.AccountUsername, orderIn.SummonerServer);
                var summonerLeagues = await _riotApiClient.GetLeagueDetailsAsync(summoner);
                //TODO: Store the summoner and league info in DB???

                var order = orderIn.ToOrder();

                if (order.IsValid(summoner, summonerLeagues))
                {

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
            public EServer SummonerServer { get; set; }
        }

        public class OrderOut
        {
            public string ClientId { get; set; }
            public string AccountUsername { get; set; }
            public string AccountPassword { get; set; }
        }
    }
}
