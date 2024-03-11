using HandmadeByDoniApp.Data.Configuration;
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

        public DbSet<Decanter> Decaners { get; set; } = null!;

        public DbSet<Box> Boxs { get; set; } = null!;

        public DbSet<Comment> Comments { get; set; } = null!;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new GlassCategoryConfiguration());

            builder.ApplyConfiguration(new CommentConfiguration());

            builder.ApplyConfiguration(new GlassConfiguration());
            builder.ApplyConfiguration(new DecanterConfiguration());
            builder.ApplyConfiguration(new BoxConfiguration());

            builder.ApplyConfiguration(new SetConfiguration());



            base.OnModelCreating(builder);
        }
    }
}