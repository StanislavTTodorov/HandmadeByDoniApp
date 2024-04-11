using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HandmadeByDoniApp.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            
            //builder
            //   .HasOne(o => o.User)
            //   .WithMany(o => o.Orders)
            //   .HasForeignKey(o => o.ClientId);
        }
    }
}
