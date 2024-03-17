

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class GlassCategoryConfiguration : IEntityTypeConfiguration<GlassCategory>
    {
        public void Configure(EntityTypeBuilder<GlassCategory> builder)
        {

            builder.HasData(SeedCategories());
        }
        private ICollection<GlassCategory> SeedCategories()
        {

            GlassCategory wineCategory = new GlassCategory()
            {
                Id = 1,
                Name = "Wine glass"
            };

            GlassCategory beerCategory = new GlassCategory()
            {
                Id = 2,
                Name = "Beer glass"
            };

            GlassCategory cognacCategory = new GlassCategory()
            {
                Id = 3,
                Name = "Cognac glass"
            };
            GlassCategory whiskeyCategory = new GlassCategory()
            {
                Id = 4,
                Name = "Whiskey glass"
            };

            GlassCategory teaCategory = new GlassCategory()
            {
                Id = 5,
                Name = "Tea cup"
            };

            GlassCategory champagneCategory = new GlassCategory()
            {
                Id = 6,
                Name = "Champagne glass"
            };

            ICollection<GlassCategory> glassCategory = new HashSet<GlassCategory>();

            glassCategory.Add(wineCategory);
            glassCategory.Add(beerCategory);
            glassCategory.Add(cognacCategory);
            glassCategory.Add(whiskeyCategory);
            glassCategory.Add(teaCategory);
            glassCategory.Add(champagneCategory);

            return glassCategory;
        }
    }
}
