using LoLBoosting.WebApi.ViewModels.Orders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LolBoosting.Services
{
    public class UserOrderRegistry
    {
        private readonly ConcurrentDictionary<string, OrderInfoIn> _guestOrderRegistry = new ConcurrentDictionary<string, OrderInfoIn>();
        private readonly ConcurrentDictionary<string, OrderInfoIn> _userOrderRegistry = new ConcurrentDictionary<string, OrderInfoIn>();

        public UserOrderRegistry()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromHours(24));

                    _guestOrderRegistry.Clear();
                    _userOrderRegistry.Clear();
                }
            });
        }

        public void RegisterGuestOrder(string ipAddress, OrderInfoIn orderInfoIn)
        {
            if (!_guestOrderRegistry.ContainsKey(ipAddress))
            {
                _guestOrderRegistry.TryAdd(ipAddress, orderInfoIn);
            }
            else
            {
                _guestOrderRegistry[ipAddress] = orderInfoIn;
            }
        }

        public void RegisterUserOrder(string userId, OrderInfoIn orderInfoIn)
        {
            if (!_userOrderRegistry.ContainsKey(userId))
            {
                _userOrderRegistry.TryAdd(userId, orderInfoIn);
            }
            else
            {
                _userOrderRegistry[userId] = orderInfoIn;
            }
        }

        public OrderInfoIn GetGuestOrder(string ipAddress)
        {
            if (_guestOrderRegistry.ContainsKey(ipAddress))
            {
                return _guestOrderRegistry[ipAddress];
            }

            return null;
        }

        public OrderInfoIn GetUserOrder(string userId)
        {
            if (_guestOrderRegistry.ContainsKey(userId))
            {
                return _guestOrderRegistry[userId];
            }

            return null;
        }
    }
}
