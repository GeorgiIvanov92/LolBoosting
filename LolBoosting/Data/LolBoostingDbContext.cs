using LolBoosting.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolBoosting.Data
{
    public class LolBoostingDbContext : ApiAuthorizationDbContext<User>
    {
        public LolBoostingDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(a => a.Orders)
                .WithOne(a => a.Booster)
                .IsRequired();

            builder.Entity<User>()
                .HasMany(a => a.Orders)
                .WithOne(a => a.Client)
                .IsRequired();
        }
    }
}
