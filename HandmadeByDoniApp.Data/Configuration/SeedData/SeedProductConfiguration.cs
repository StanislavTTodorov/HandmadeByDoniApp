

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Net.WebRequestMethods;

namespace HandmadeByDoniApp.Data.Configuration.SeedData
{
    public class SeedProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(SeedCategories());
        }
        private ICollection<Product> SeedCategories()
        {
            ICollection<Product> products = new HashSet<Product>();
            Product queen = new Product()
            {
                Id = Guid.Parse("2B07A00D-9963-4074-8785-2DED25FACA31"),
                Title = "The Queen",
                Description = "Beer glass The Queen 1 glass",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_213051%20(Copy).jpg",
                Price = 70.00M,
                CategoryId = 2,
                CreatedOn = DateTime.Parse("2024-03-18 10:55:01.8466667"),
                IsActive = true,
            };
            products.Add( queen );         

            Product royalPower = new Product()
            {
                Id = Guid.Parse("62848C82-CF7B-4367-ADCE-6779103E87F6"),
                Title = "Royal Power",
                Description = "Royal Power 1 cup",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20211204_153642%20(Copy).jpg",
                Price = 65.00M,
                CategoryId = 1,
                CreatedOn = DateTime.Parse("2024-03-27 12:41:56.1433333"),             
                IsActive = false,
               //OrderId = Guid.Parse("EE9D71DF-D7E4-4F85-A53E-07BFE35C0208")
                
            };
            products.Add( royalPower );

            Product mermaid = new Product()
            {
                Id = Guid.Parse("C8198BC1-2A95-460F-A56A-711042A71F19"),
                Title = "Mermaid",
                Description = "Mermaid 1 cup",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20211204_153938%20(Copy).jpg",
                Price = 65.00M,
                CategoryId = 1,
                CreatedOn = DateTime.Parse("2024-03-18 17:49:43.3300000"),
                IsActive = true,
            };
            products.Add( mermaid );

          

            Product firebird = new Product()
            {
                Id = Guid.Parse("1F311AED-E45A-4499-9A99-937503EAC6FB"),
                Title = "Firebird 'Жар птица' ",
                Description = "Firebird 1 cup",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_205953%20(Copy).jpg",
                Price = 45m,
                CategoryId = 1,
                CreatedOn = DateTime.Parse("2024-03-18 18:03:29.2466667"),
                IsActive = true
            };
            products.Add(firebird );

            Product madonna = new Product()
            {
                Id = Guid.Parse("2D249AD1-FCF5-431E-9DBE-9E18A101BA8E"),
                Title = "The Madonna and Child' ",
                Description = "The Madonna and Child 1 cup",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20231218_162414%20().jpg",
                Price = 67m,
                CategoryId = 1,
                CreatedOn = DateTime.Parse("2024-03-15 21:27:18.7900000"),
                IsActive = true,
            };
            products.Add ( madonna );
         

            return products;
        }
    }
}
