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
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }
        public LolBoostingDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserOrder>().HasKey(uo => new {uo.OrderId, uo.UserId});

            builder.Entity<UserOrder>().HasOne(uo => uo.Order)
                .WithMany(uo => uo.UserOrders)
                .HasForeignKey(uo => uo.OrderId);

            builder.Entity<UserOrder>().HasOne(uo => uo.User)
                .WithMany(uo => uo.UserOrders)
                .HasForeignKey(uo => uo.UserId);

            //builder.Entity<Order>()
            //    .HasMany(a => a.Users)
            //    .WithOne(
            //    .HasForeignKey(a => a.ClientId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<Order>()
            //    .HasOne(a => a.Booster)
            //    .WithMany()
            //    .HasForeignKey(a => a.BoosterId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>()
                .Property(p => p.BoosterEarningsPer20LP)
                .HasColumnType("decimal(5,2)");

            builder.Entity<Order>()
                .Property(p => p.BoosterEarningsPerGame)
                .HasColumnType("decimal(5,2)");

            builder.Entity<Order>()
                .Property(p => p.BoosterEarningsPerWin)
                .HasColumnType("decimal(5,2)");

            builder.Entity<User>()
                .Property(p => p.Balance)
                .HasColumnType("decimal(5,2)");

            builder.Entity<User>().Property(p => p.ImageData).IsRequired(false);

        }
    }
}
