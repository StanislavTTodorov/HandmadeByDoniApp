

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HandmadeByDoniApp.Data.Configuration.SeedData
{
    public class SeedSetConfiguration : IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> builder)
        {
            builder.HasData(SeedSets());
        }
        private ICollection<Set> SeedSets()
        {
            Set angels =new Set() 
            { 
                Id = Guid.Parse("F57A2A6C-4DDD-46F9-BD6E-8A3FAF801527"),
                Title = "Two angels 2 cups",
                Description = "Two angels 2 cups",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/Cognac%20(Copy).jpg",
                Price = 105m,
                IsActive = true,
                CreatedOn = DateTime.Parse("2024-04-07 12:06:18.1628495")

            };
            Set sineva = new Set()
            {
                Id = Guid.Parse("BFA2762A-617C-4119-8462-D599D9588B61"),
                Title = "Sineva",
                Description = "Sineva - 1 decanter with 2 glasses",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_205122%20(Copy).jpg",
                Price = 120m,
                IsActive = true,
                CreatedOn = DateTime.Parse("2024-04-08 12:39:12.3871453")
            };
            ICollection<Set> sets = new HashSet<Set>();

            sets.Add(angels);
            sets.Add(sineva);

            return sets;
        }
    }
}
