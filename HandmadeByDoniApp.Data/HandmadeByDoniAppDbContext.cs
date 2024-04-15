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
        public DbSet<Glass> Glasses { get; set; } = null!;

        public DbSet<GlassCategory> GlassCategories { get; set; } = null!;

        public DbSet<Set> Sets { get; set; } = null!;

        public DbSet<Decanter> Decantres { get; set; } = null!;

        public DbSet<Box> Boxs { get; set; } = null!;

        public DbSet<Comment> Comments { get; set; } = null!;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<UserOrder> UsersOrders { get; set; } = null!;

        public DbSet<Address> Addresses { get; set; } = null!;

        public DbSet<DeliveryCompany>  DeliveryCompanies { get; set; } = null!;

        public DbSet<MethodPayment> MethodPayments { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new SetConfiguration());

            builder.ApplyConfiguration(new GlassConfiguration());
            builder.ApplyConfiguration(new DecanterConfiguration());
            builder.ApplyConfiguration(new BoxConfiguration());

            builder.ApplyConfiguration(new CommentConfiguration());
            
            builder.ApplyConfiguration(new UserOrderConfiguration());

            bool seed = false;
           if(seed)
            {
                builder.ApplyConfiguration(new SeedGlassCategoryConfiguration());
                builder.ApplyConfiguration(new SeedMethodPaymentConfiguration());
                builder.ApplyConfiguration(new SeedDeliveryCompanyConfiguration());

                builder.ApplyConfiguration(new SeedUserConfiguration());
                builder.ApplyConfiguration(new SeedOrderConfiguration());

                builder.ApplyConfiguration(new SeedSetConfiguration());

                builder.ApplyConfiguration(new SeedGlassConfiguration());
                builder.ApplyConfiguration(new SeedDecanterConfiguration());
                builder.ApplyConfiguration(new SeedBoxConfiguration());

                builder.ApplyConfiguration(new SeedAddressConfiguration());
                builder.ApplyConfiguration(new SeedCommentConfiguration());

                builder.ApplyConfiguration(new SeedUserOrderConfiguration());
            }
            base.OnModelCreating(builder);
        }
    }
}