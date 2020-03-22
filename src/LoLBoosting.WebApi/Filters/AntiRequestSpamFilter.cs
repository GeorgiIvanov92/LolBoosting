using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LolBoosting.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LoLBoosting.WebApi.Filters
{
    public class AntiRequestSpamFilter : IActionFilter
    {
        private readonly UserRequestRegistry _userRequestRegistry;
        public AntiRequestSpamFilter(UserRequestRegistry userRequestRegistry)
        {
            _userRequestRegistry = userRequestRegistry;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_userRequestRegistry.RegisterRequest(context.HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                context.Result = new BadRequestObjectResult("Too many requests! Wait 10 seconds and try again");
            }
        }
    }
}
