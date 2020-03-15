using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace LoLBoosting.RiotApi.Services
{
    public class Worker : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
