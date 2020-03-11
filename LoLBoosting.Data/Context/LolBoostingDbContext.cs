using IdentityServer4.EntityFramework.Options;
using LolBoosting.Models;
using LoLBoosting.Data.Context.EntityConfigurations;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LolBoosting.Data.Context
{
    public class LolBoostingDbContext : ApiAuthorizationDbContext<User>
    {       
        public LolBoostingDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new OrderEntityTypeConfiguration());



            //builder.Entity<User>()
            //    .Property(p => p.Balance)
            //    .HasColumnType("decimal(5,2)");

            //builder.Entity<User>().Property(p => p.ImageData).IsRequired(false);
        }
    }
}
