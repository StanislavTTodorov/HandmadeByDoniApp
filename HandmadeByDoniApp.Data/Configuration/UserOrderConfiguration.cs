

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class UserOrderConfiguration : IEntityTypeConfiguration<UserOrder>
    {
        public void Configure(EntityTypeBuilder<UserOrder> builder)
        {
            builder.HasKey(pk => new { pk.UserId, pk.OrderId });

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Order)
                .WithMany()
                .HasForeignKey(x => x.OrderId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Address)
                   .WithMany()
                   .HasForeignKey(x=>x.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(h => h.IsSent)
               .HasDefaultValue(false);

        }      
    }
}
