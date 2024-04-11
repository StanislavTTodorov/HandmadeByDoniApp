

using HandmadeByDoniApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasMany(x => x.Glasses)
                .WithMany(x => x.ApplicationUsers);

            builder
               .HasMany(x => x.Sets)
               .WithMany(x => x.ApplicationUsers);
            builder
               .HasMany(x => x.Boxs)
               .WithMany(x => x.ApplicationUsers);
            builder
               .HasMany(x => x.Decanters)
               .WithMany(x => x.ApplicationUsers);
        }
    }
}
