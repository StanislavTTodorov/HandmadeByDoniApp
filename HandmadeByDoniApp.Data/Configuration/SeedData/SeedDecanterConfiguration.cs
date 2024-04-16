

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration.SeedData
{
    public class SeedDecanterConfiguration : IEntityTypeConfiguration<Decanter>
    {
        public void Configure(EntityTypeBuilder<Decanter> builder)
        {
            builder.HasData(SeedDecanters());
        }

        private ICollection<Decanter> SeedDecanters()
        {
           ICollection<Decanter> decanters = new HashSet<Decanter>();
            Decanter lion = new Decanter()
            {
                Id=Guid.Parse("C7C5D614-A354-420A-A816-10FF8E3BD391"),
                Title = "The glory of the lion",
                Description = "The glory of the lion",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20230724_111227%20(Copy).jpg",
                Capacity = 750,
                Price = 57M,
                CreatedOn = DateTime.Parse("2024-03-18 18:09:02.1166667"),
                IsActive = true,
                IsSet = false,              
            };
            decanters.Add(lion);
            Decanter madonna = new Decanter()
            {
                Id = Guid.Parse("2CCBD57F-25AB-4DBB-9EC0-238839422E87"),
                Title = "The Madonna",
                Description = "The Madonna",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20230724_112332%20(Copy).jpg",
                Capacity = 400,
                Price = 82M,
                CreatedOn = DateTime.Parse("2024-03-18 17:52:29.2833333"),
                IsActive = true,
                IsSet = false,
            };
            decanters.Add(madonna);

            Decanter angels = new Decanter()
            {
                Id = Guid.Parse("E62E9313-837B-4A77-8D9F-2F6B21993284"),
                Title = "Angels with crowns",
                Description = "Angels with crowns",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20230501_224607%20().jpg",
                Capacity = 750,
                Price = 79M,
                CreatedOn = DateTime.Parse("2024-03-15 18:34:53.7700000"),
                IsActive = true,
                IsSet = false,
            };
            decanters.Add(angels);

            Decanter sineva = new Decanter()
            {
                Id = Guid.Parse("6679B8AE-C473-4514-B5AB-8608DD5C537D"),
                Title = "Sineva",
                Description = "",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_204938%20(Copy).jpg",
                Capacity = 1200,
                Price = 0M,
                CreatedOn = DateTime.Parse("2024-04-08 12:39:12.3877104"),
                IsActive = true,
                IsSet = true,
                
             
            };
            decanters.Add(sineva);

            Decanter thrace = new Decanter()
            {
                Id = Guid.Parse("4EF3846B-6C81-4B91-A702-C69B399EF550"),
                Title = "Thrace",
                Description = "Thrace",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20230724_113929%20(Copy).jpg",
                Capacity = 1200,
                Price = 50M,
                CreatedOn = DateTime.Parse("2024-03-22 09:26:30.5600000"),
                IsActive = false,
                IsSet = false,
                OrderId = Guid.Parse("F95D5A5C-4B30-4453-8F3B-5BCE14142DCC")
            };
            decanters.Add(thrace);

            return decanters;
        }
    }
}
