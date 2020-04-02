using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LoLBoosting.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoLBoosting.Data.Repository;
using LoLBoosting.WebApi.Extensions;
using LoLBoosting.Contracts.Models;
using LoLBoosting.WebApi.Communication.Http;
using LoLBoosting.Contracts.Orders;
using LoLBoosting.Entities;
using LolBoosting.Services;
using LoLBoosting.WebApi.Filters;
using LoLBoosting.WebApi.ViewModels.Orders;

namespace LoLBoosting.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private IRepository<Order> _orderRepository;
        private UserManager<User> _userManager;
        private readonly RiotApiClient _riotApiClient;
        private readonly MultiplyCalculator _multiplyCalculator;
        private const string RankedSoloQueue = "RANKED_SOLO_5x5";
        private readonly IRepository<TierRate> _tierRateRepository;
        private readonly UserOrderRegistry _userOrderRegistry;
        private readonly List<ETier> _foribiddenTiers = new List<ETier>
        {
            ETier.Challenger,
            ETier.GrandMaster
        };

        public OrderController(IDeletebleEntityRepository<Order> orderRepositpory,
            IRepository<TierRate> tierRateRepository,
            UserManager<User> userManager,
            RiotApiClient riotApiClient,
            MultiplyCalculator multiplyCalculator,
            UserOrderRegistry userOrderRegistry)
        {
            _orderRepository = orderRepositpory;
            _userManager = userManager;
            _riotApiClient = riotApiClient;
            _multiplyCalculator = multiplyCalculator;
            _tierRateRepository = tierRateRepository;
            _userOrderRegistry = userOrderRegistry;
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


        [HttpPost]
        [ServiceFilter(typeof(AntiRequestSpamFilter))]
        [Route("CalculatePrice")]
        [ProducesResponseType(typeof(OrderMetadataOut), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OrderMetadataOut>> CalculatePrice([FromBody] OrderInfoIn orderInfo)
        {
            if (ModelState.IsValid)
            {
                var orderMetadata = GetOrderMetadata(orderInfo);

                if(orderMetadata != null)
                {
                    return Ok(orderMetadata);
                }
            }

            return BadRequest("No such user found or Division is not eligable for boosting");
        }

        [HttpPost]
        [Route("ProceedToPayment")]
        [ProducesResponseType((int) HttpStatusCode.Redirect)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ProceedToPayment(OrderInfoIn orderInfo)
        {
            try
            {
                if (!this.User.Claims.Any())
                {
                    _userOrderRegistry.RegisterGuestOrder(this.HttpContext.Connection.RemoteIpAddress.ToString(), orderInfo);
                }
                else
                {
                    _userOrderRegistry.RegisterUserOrder(this.User.Claims.FirstOrDefault(z => z.Type == "sub").Value, orderInfo);
                }

                return Ok();
            }
            catch(Exception ex)
            {
               
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("GetUserOrder")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<OrderMetadataOut>> GetUserOrder()
        {
            try
            {
                var userOrder = _userOrderRegistry.GetUserOrder(this.User.Claims.FirstOrDefault(z => z.Type == "sub").Value);

                if(userOrder == null)
                {
                    userOrder = _userOrderRegistry.GetGuestOrder(this.HttpContext.Connection.RemoteIpAddress.ToString());
                }

                if(userOrder == null)
                {
                    return NoContent();
                }

                var orderMetadata = GetOrderMetadata(userOrder);

                orderMetadata.Price *= userOrder.NumberOfGames;

                return Ok(orderMetadata);
            }
            catch(Exception ex)
            {
                
            }

            return NoContent();
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

        private  OrderMetadataOut GetOrderMetadata(OrderInfoIn orderInfo)
        { 
            var summoner = _riotApiClient.GetSummonerDetailsAsync(orderInfo.Username, orderInfo.Server).Result;

            var summonerLeagues = _riotApiClient.GetLeagueDetailsAsync(summoner).Result;

            var soloQueueLeague = summonerLeagues?.FirstOrDefault(l =>
                l.QueueType.Equals(RankedSoloQueue, StringComparison.InvariantCultureIgnoreCase));

            if (soloQueueLeague != null)
            {
                Enum.TryParse(soloQueueLeague.Tier, true, out ETier tier);
                Enum.TryParse(soloQueueLeague.Rank, true, out EDivision division);

                if (_foribiddenTiers.Contains(tier))
                {
                    return null;
                }

                var multiplier =
                    _multiplyCalculator.GetMultiplier(division, soloQueueLeague.LeaguePoints, orderInfo.OrderType);
                var rate = _tierRateRepository.Find(tier);

                var price = multiplier * rate.Price;

                return new OrderMetadataOut
                {
                    CurrentDivision = division,
                    CurrentPoints = soloQueueLeague.LeaguePoints,
                    CurrentTier = tier,
                    Price = price
                };
            }

            return null;
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
