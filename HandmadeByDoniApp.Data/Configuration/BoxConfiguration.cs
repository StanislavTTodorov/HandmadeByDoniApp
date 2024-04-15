

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class BoxConfiguration : IEntityTypeConfiguration<Box>
    {
        public void Configure(EntityTypeBuilder<Box> builder)
        {
            builder.Property(h => h.CreatedOn)
                 .HasDefaultValueSql("GETDATE()");

;

           // builder.HasData(SeedBoxs());

        }

        private ICollection<Box> SeedBoxs()
        {
            ICollection<Box> boxes = new HashSet<Box>();
            Box firstBox = new Box()
            {
                Id = Guid.Parse("8DC60AF6-5E52-4B64-8E3A-343FB3D425FD"),
                Title = "Box",
                Description = "Box for 6 glasses",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20220212_221050.jpg?t=2024-03-18T10%3A17%3A47.754Z",
                Capacity = 6,
                Price = 60m,
                CreatedOn = DateTime.Parse("2024-03-17 07:28:01.3133333"),
                IsActive = true
                
            };
            boxes.Add(firstBox);

            Box secondBox = new Box()
            {
                Id = Guid.Parse("7E302B06-93B6-493D-8784-4DA6EB5C91B8"),
                Title = "Box",
                Description = "Box for 2 glasses",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20220212_211449.jpg",
                Capacity = 2,
                Price = 20m,
                CreatedOn = DateTime.Parse("2024-03-18 18:05:28.1533333"),
                IsActive = true

            };
            boxes.Add(secondBox);

            Box thirdBox = new Box()
            {
                Id = Guid.Parse("9DFE60C9-5292-4DC5-9365-DC0864F40D4B"),
                Title = "Box",
                Description = "Box for 4 glasses",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20220212_214106.jpg?t=2024-03-18T10%3A16%3A38.799Z",
                Capacity = 4,
                Price = 40m,
                CreatedOn = DateTime.Parse("2024-03-17 07:31:23.2066667"),
                IsActive = true

            };
            boxes.Add(thirdBox);

            return boxes;

        }
    }
}
