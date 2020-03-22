using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LolBoosting.Services
{
    public class UserRequestRegistry
    {
        private const int MaxRequestCountInShortPeriod = 10;
        private readonly TimeSpan _shortPeriod = TimeSpan.FromSeconds(10);
        private ConcurrentDictionary<string, UserRequestInfo> _userRequests = new ConcurrentDictionary<string, UserRequestInfo>();

        public UserRequestRegistry()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromSeconds(2));

                    var userIdsToRemove = new List<string>();

                    foreach (var user in _userRequests)
                    {
                        if (user.Value.LastRequestDateTime < DateTime.UtcNow - _shortPeriod)
                        {
                            userIdsToRemove.Add(user.Key);
                        }
                    }

                    foreach (var userIdToRemove in userIdsToRemove)
                    {
                        _userRequests.TryRemove(userIdToRemove, out var user);
                    }
                }
            });
        }

        public bool RegisterRequest(string ipAddress)
        {
            if (!_userRequests.ContainsKey(ipAddress))
            {
                _userRequests.TryAdd(ipAddress, new UserRequestInfo());
            }

            _userRequests[ipAddress].LastRequestDateTime = DateTime.UtcNow;
            _userRequests[ipAddress].RequestCounter++;

            return _userRequests[ipAddress].RequestCounter < MaxRequestCountInShortPeriod;
        }

        private class UserRequestInfo
        {
            public DateTime LastRequestDateTime { get; set; } = DateTime.MinValue;
            public int RequestCounter { get; set; }
        }
    }
}
