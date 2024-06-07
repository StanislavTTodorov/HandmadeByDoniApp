

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration.SeedData
{
    public class SeedCategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }
        private ICollection<Category> SeedCategories()
        {

            Category wineCategory = new Category()
            {
                Id = 1,
                Name = "Wine glass"
            };

            Category beerCategory = new Category()
            {
                Id = 2,
                Name = "Beer glass"
            };

            Category cognacCategory = new Category()
            {
                Id = 3,
                Name = "Cognac glass"
            };
            Category whiskeyCategory = new Category()
            {
                Id = 4,
                Name = "Whiskey glass"
            };

            Category teaCategory = new Category()
            {
                Id = 5,
                Name = "Tea cup"
            };

            Category champagneCategory = new Category()
            {
                Id = 6,
                Name = "Champagne glass"
            };
            Category decanterCategory = new Category()
            {
                Id = 7,
                Name = "Decanter"
            };

            ICollection<Category> glassCategory = new HashSet<Category>();

            glassCategory.Add(wineCategory);
            glassCategory.Add(beerCategory);
            glassCategory.Add(cognacCategory);
            glassCategory.Add(whiskeyCategory);
            glassCategory.Add(teaCategory);
            glassCategory.Add(champagneCategory);
            glassCategory.Add(decanterCategory);

            return glassCategory;
        }
    }
}
