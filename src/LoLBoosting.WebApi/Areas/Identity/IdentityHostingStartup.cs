using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LoLBoosting.WebApi.Areas.Identity.IdentityHostingStartup))]
namespace LoLBoosting.WebApi.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}