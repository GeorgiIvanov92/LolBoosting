using IdentityServer4.EntityFramework.Options;
using LoLBoosting.Models;
using LoLBoosting.Data.Context.EntityConfigurations;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LoLBoosting.Data.Context
{
    public class LolBoostingDbContext : ApiAuthorizationDbContext<User>
    {
        public LolBoostingDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            builder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
