using HandmadeByDoniApp.Data.Configuration;
using HandmadeByDoniApp.Data.Configuration.SeedData;
using HandmadeByDoniApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Data
{
    public class HandmadeByDoniAppDbContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>
    {
        public HandmadeByDoniAppDbContext(DbContextOptions<HandmadeByDoniAppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Comment> Comments { get; set; } = null!;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<UserOrder> UsersOrders { get; set; } = null!;

        public DbSet<Address> Addresses { get; set; } = null!;

        public DbSet<DeliveryCompany>  DeliveryCompanies { get; set; } = null!;

        public DbSet<MethodPayment> MethodPayments { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new CommentConfiguration());
            
            builder.ApplyConfiguration(new UserOrderConfiguration());

            bool seed = false;
           if(seed)
            {
                builder.ApplyConfiguration(new SeedCategoryConfiguration());

                builder.ApplyConfiguration(new SeedMethodPaymentConfiguration());
                builder.ApplyConfiguration(new SeedDeliveryCompanyConfiguration());

                builder.ApplyConfiguration(new SeedUserConfiguration());
 
                builder.ApplyConfiguration(new SeedProductConfiguration());
            }
            base.OnModelCreating(builder);
        }
    }
}