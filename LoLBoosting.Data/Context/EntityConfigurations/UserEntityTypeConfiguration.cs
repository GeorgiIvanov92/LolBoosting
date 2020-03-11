using LolBoosting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoLBoosting.Data.Context.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Balance)
                .HasColumnType("decimal(5,2)");

            //builder.Entity<User>().Property(p => p.ImageData).IsRequired(false);
        }
    }
}
