using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly UserRequestRegistry _userRequestRegistry;
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
            UserRequestRegistry userRequestRegistry)
        {
            _orderRepository = orderRepositpory;
            _userManager = userManager;
            _riotApiClient = riotApiClient;
            _multiplyCalculator = multiplyCalculator;
            _tierRateRepository = tierRateRepository;
            _userRequestRegistry = userRequestRegistry;
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
        [Authorize(Roles = "Client,Administrator")]
        [ProducesResponseType(typeof(OrderMetadataOut), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OrderMetadataOut>> CalculatePrice([FromBody] OrderInfoIn orderInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var summoner =
                        await _riotApiClient.GetSummonerDetailsAsync(orderInfo.Username, orderInfo.Server);

                    var summonerLeagues = await _riotApiClient.GetLeagueDetailsAsync(summoner);

                    var soloQueueLeague = summonerLeagues?.FirstOrDefault(l =>
                        l.QueueType.Equals(RankedSoloQueue, StringComparison.InvariantCultureIgnoreCase));

                    if (soloQueueLeague != null)
                    {
                        Enum.TryParse(soloQueueLeague.Tier, true, out ETier tier);
                        Enum.TryParse(soloQueueLeague.Rank, true, out EDivision division);

                        if (_foribiddenTiers.Contains(tier))
                        {
                            return BadRequest("Forbidden Tier!");
                        }

                        var multiplier =
                            _multiplyCalculator.GetMultiplier(division, soloQueueLeague.LeaguePoints, orderInfo.OrderType);
                        var rate = _tierRateRepository.Find(tier);

                        var price = multiplier * rate.Price;

                        return Ok(new OrderMetadataOut
                        {
                            CurrentDivision = division,
                            CurrentPoints = soloQueueLeague.LeaguePoints,
                            CurrentTier = tier,
                            Price = price
                        });
                    }
                    else if (summoner != null)
                    {
                        //TODO: get last season tier and division

                        //var multiplier =
                        //    _multiplyCalculator.GetMultiplier(EDivision.Undefined, soloQueueLeague.LeaguePoints, orderType);
                        //var rate = _tierRateRepository.Find(ETier.Unranked);

                        //var price = multiplier * rate.Price;

                        //return Ok(new OrderMetadataOut
                        //{
                        //    CurrentDivision = EDivision.Undefined,
                        //    CurrentPoints = soloQueueLeague.LeaguePoints,
                        //    CurrentTier = ETier.Unranked,
                        //    Price = price
                        //});
                    }
                }
                catch(Exception ex)
                {
                    
                }
            }

            return BadRequest("No such user found!");
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

        public class OrderInfoIn
        {
            public string Username { get; set; }
            public EServer Server { get; set; }
            public EOrderType OrderType { get; set; }
        }
        public class OrderMetadataOut
        {
            public double Price { get; set; }
            public ETier CurrentTier { get; set; }
            public EDivision CurrentDivision { get; set; }
            public double CurrentPoints { get; set; }
        }
    }
}
