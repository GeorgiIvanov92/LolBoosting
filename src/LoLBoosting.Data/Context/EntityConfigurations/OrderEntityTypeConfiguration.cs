using LoLBoosting.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoLBoosting.Data.Context.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.OrderId);

            builder.Property(e => e.AccountUsername)
                .IsRequired();

            builder.Property(e => e.AccountPassword)
                .IsRequired();

            //builder.Entity<Order>()
            //    .Property(p => p.BoosterEarningsPer20LP)
            //    .HasColumnType("decimal(5,2)");

            //builder.Entity<Order>()
            //    .Property(p => p.BoosterEarningsPerGame)
            //    .HasColumnType("decimal(5,2)");

            //builder.Entity<Order>()
            //    .Property(p => p.BoosterEarningsPerWin)
            //    .HasColumnType("decimal(5,2)");

            builder.HasOne(e => e.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<Order>()
            //    .HasOne(a => a.Booster)
            //    .WithMany()
            //    .HasForeignKey(a => a.BoosterId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
