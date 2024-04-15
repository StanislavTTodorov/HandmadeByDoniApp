

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

           // builder.HasData(SeedUsers());
        }
        private ICollection<ApplicationUser> SeedUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            ApplicationUser admin = new ApplicationUser()
            {
                Id = Guid.Parse("80255D94-AEFE-4C1D-ABB6-715604DB71B0"),
                UserName = "admin@handmadebydoni.bg",
                NormalizedUserName = "ADMIN@HANDMADEBYDONI.BG",
                Email = "admin@handmadebydoni.bg",
                NormalizedEmail = "ADMIN@HANDMADEBYDONI.BG",
                EmailConfirmed = false,
                SecurityStamp = "ZLCGPEWE3P3BDNVK526PG2IX6B6N6N44",
                ConcurrencyStamp = "523d2c69-b025-49e2-b98b-5d6740549418",
                FirstName = "Admin",
                LastName = "Admin"
               

            };

            admin.PasswordHash = hasher.HashPassword(admin, "admin123");

            ApplicationUser firstUser = new ApplicationUser()
            {
                Id = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
                UserName = "Rali@gmail.com",
                NormalizedUserName = "RALI@GMAIL.COM",
                Email = "Rali@gmail.com",
                NormalizedEmail = "RALI@GMAIL.COM",
                EmailConfirmed = false,
                SecurityStamp = ("7HKI4MSRKJKX2DDLAGLVXU7UGKIJVNIR"),
                ConcurrencyStamp = ("cc3f7fbb-f77e-40b3-aeaa-8cc9d75245ea"),
                FirstName = "Ralka",
                LastName = "Slavova"

            };
            firstUser.PasswordHash = hasher.HashPassword(firstUser, "user123");

            ApplicationUser seconаUser = new ApplicationUser()
            {
                Id = Guid.Parse("371900A3-A5D5-422D-815D-C1D9228C11D0"),
                UserName = "boris@gmail.com",
                NormalizedUserName = "BORIS@GMAIL.COM",
                Email = "boris@gmail.com",
                NormalizedEmail = "BORIS@GMAIL.COM",
                EmailConfirmed = false,
                SecurityStamp = ("37DLJDOBTDVEYX7UIRMCHQ47DPPW5C3I"),
                ConcurrencyStamp = ("8fee1acc-b827-4cb4-a53a-bfbade046f31"),
                FirstName = "Bobi",
                LastName = "Borisov"

            };
            seconаUser.PasswordHash = hasher.HashPassword(seconаUser, "user123");
            ICollection<ApplicationUser> users = new HashSet<ApplicationUser>();

            users.Add(admin);
            users.Add(firstUser);
            users.Add(seconаUser);

            return users;
        }
    }
}
