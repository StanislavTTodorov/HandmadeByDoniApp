

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

          //  builder.HasData(SeedUserOrders());
        }

        private ICollection<UserOrder> SeedUserOrders()
        {
            ICollection<UserOrder> userOrders = new HashSet<UserOrder>();

            UserOrder firstUserOrder = new UserOrder() 
            { 
                UserId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
                OrderId = Guid.Parse("EE9D71DF-D7E4-4F85-A53E-07BFE35C0208"),
                TotalPrice = 65m,
                CreaateOn = DateTime.Parse("2024-04-14 07:36:22.5336115"),
                AddressId = Guid.Parse("E2134209-BFE1-4AD3-8B89-E3C8F95B55C0"),
                IsSent = false,               
            };
            userOrders.Add(firstUserOrder);

            UserOrder secondUserOrder = new UserOrder()
            {
                UserId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
                OrderId = Guid.Parse("F95D5A5C-4B30-4453-8F3B-5BCE14142DCC"),
                TotalPrice = 50m,
                CreaateOn = DateTime.Parse("2024-04-12 09:06:27.8338791"),
                AddressId = Guid.Parse("E2134209-BFE1-4AD3-8B89-E3C8F95B55C0"),
                IsSent = true,
            };
            userOrders.Add(secondUserOrder);

            return userOrders;
        }
    }
}
