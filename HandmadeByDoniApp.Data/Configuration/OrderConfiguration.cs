using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HandmadeByDoniApp.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

           // builder.HasData(SeedOrders());
        }
        private ICollection<Order> SeedOrders()
        {
            Order firstOrder = new Order()
            {
                Id = Guid.Parse("EE9D71DF-D7E4-4F85-A53E-07BFE35C0208"),
                ClientId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
            };

            Order secondOrder = new Order()
            {
                Id = Guid.Parse("F95D5A5C-4B30-4453-8F3B-5BCE14142DCC"),
                ClientId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
            };
            ICollection<Order> orders = new HashSet<Order>();

            orders.Add(firstOrder);
            orders.Add(secondOrder);

            return orders;

        }
    }
}
