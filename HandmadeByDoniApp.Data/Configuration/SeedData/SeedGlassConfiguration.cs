

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Net.WebRequestMethods;

namespace HandmadeByDoniApp.Data.Configuration.SeedData
{
    public class SeedGlassConfiguration : IEntityTypeConfiguration<Glass>
    {
        public void Configure(EntityTypeBuilder<Glass> builder)
        {
            builder.HasData(SeedCategories());
        }
        private ICollection<Glass> SeedCategories()
        {
            ICollection<Glass> glasses = new HashSet<Glass>();
            Glass queen = new Glass()
            {
                Id = Guid.Parse("2B07A00D-9963-4074-8785-2DED25FACA31"),
                Title = "The Queen",
                Description = "Beer glass The Queen 1 glass",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_213051%20(Copy).jpg",
                Capacity = 500,
                Price = 70.00M,
                GlassCategoryId = 2,
                CreatedOn = DateTime.Parse("2024-03-18 10:55:01.8466667"),
                IsActive = true,
                IsSet = false,
            };
            glasses.Add( queen );

            Glass angels1 = new Glass()
            {
                Id = Guid.Parse("E72293AA-E080-4B0B-B72E-31ADC1ECDE96"),
                Title = "Two angels",
                Description = "",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20231212_125737%20(Copy).jpg",
                Capacity = 150,
                Price = 0M,
                GlassCategoryId = 3,
                CreatedOn = DateTime.Parse("2024-04-07 12:06:12.8271188"),
                IsActive = true,
                IsSet = true,
                SetId = Guid.Parse("F57A2A6C-4DDD-46F9-BD6E-8A3FAF801527"),                             
            };

            glasses.Add ( angels1 );

            Glass angels2 = new Glass()
            {
                Id = Guid.Parse("71410857-73DB-48B1-B1BC-453BCE46706C"),
                Title = "Two angels",
                Description = "",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20231212_125737%20(Copy).jpg",
                Capacity = 150,
                Price = 0M,
                GlassCategoryId = 3,
                CreatedOn = DateTime.Parse("2024-04-07 12:06:12.8261188"),
                IsActive = true,
                IsSet = true,
                SetId = Guid.Parse("F57A2A6C-4DDD-46F9-BD6E-8A3FAF801527"),
            };
            glasses.Add(angels2);

            Glass royalPower = new Glass()
            {
                Id = Guid.Parse("62848C82-CF7B-4367-ADCE-6779103E87F6"),
                Title = "Royal Power",
                Description = "Royal Power 1 cup",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20211204_153642%20(Copy).jpg",
                Capacity = 350,
                Price = 65.00M,
                GlassCategoryId = 1,
                CreatedOn = DateTime.Parse("2024-03-27 12:41:56.1433333"),             
                IsActive = false,
                IsSet = false,
                OrderId = Guid.Parse("EE9D71DF-D7E4-4F85-A53E-07BFE35C0208")
                
            };
            glasses.Add( royalPower );

            Glass mermaid = new Glass()
            {
                Id = Guid.Parse("C8198BC1-2A95-460F-A56A-711042A71F19"),
                Title = "Mermaid",
                Description = "Mermaid 1 cup",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20211204_153938%20(Copy).jpg",
                Capacity = 350,
                Price = 65.00M,
                GlassCategoryId = 1,
                CreatedOn = DateTime.Parse("2024-03-18 17:49:43.3300000"),
                IsActive = true,
                IsSet = false,
            };
            glasses.Add( mermaid );

            Glass sineva1 = new Glass()
            {
                Id = Guid.Parse("AD666900-3451-47A4-A8B8-75FCE9071005"),
                Title = "Sineva",
                Description = "",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_205034%20(Copy).jpg",
                Capacity = 200,
                Price = 0m,
                GlassCategoryId = 1,
                CreatedOn = DateTime.Parse("2024-04-08 12:39:12.3874015"),
                SetId = Guid.Parse("BFA2762A-617C-4119-8462-D599D9588B61"),
                IsActive = true,
                IsSet = true,               
            };
            glasses.Add(sineva1 );

            Glass firebird = new Glass()
            {
                Id = Guid.Parse("1F311AED-E45A-4499-9A99-937503EAC6FB"),
                Title = "Firebird 'Жар птица' ",
                Description = "Firebird 1 cup",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_205953%20(Copy).jpg",
                Capacity = 300,
                Price = 45m,
                GlassCategoryId = 1,
                CreatedOn = DateTime.Parse("2024-03-18 18:03:29.2466667"),
                IsActive = true,
                IsSet = false
            };
            glasses.Add(firebird );

            Glass madonna = new Glass()
            {
                Id = Guid.Parse("2D249AD1-FCF5-431E-9DBE-9E18A101BA8E"),
                Title = "The Madonna and Child' ",
                Description = "The Madonna and Child 1 cup",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20231218_162414%20().jpg",
                Capacity = 400,
                Price = 67m,
                GlassCategoryId = 1,
                CreatedOn = DateTime.Parse("2024-03-15 21:27:18.7900000"),
                IsActive = true,
                IsSet = false
            };
            glasses.Add ( madonna );

            Glass sineva2 = new Glass()
            {
                Id = Guid.Parse("D16C0E0E-348E-42E3-BEEF-F90FB4FE1216"),
                Title = "Sineva",
                Description = "",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_205034%20(Copy).jpg",
                Capacity = 200,
                Price = 0m,
                GlassCategoryId = 1,
                CreatedOn = DateTime.Parse("2024-04-08 12:39:12.3874830"),
                SetId = Guid.Parse("BFA2762A-617C-4119-8462-D599D9588B61"),
                IsActive = true,
                IsSet = true,
            };
            glasses.Add(sineva2);

            return glasses;
        }
    }
}
