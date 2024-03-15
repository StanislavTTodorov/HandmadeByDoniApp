

using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data
{
    public class GlassCategoryService : IGlassCategoryService
    {
        private readonly HandmadeByDoniAppDbContext dbContext;
        public GlassCategoryService(HandmadeByDoniAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<GlassSelectCategoryFormModel>> AllGlassCategoriesAsync()
        {
            IEnumerable<GlassSelectCategoryFormModel> allGlassCategory =
            await this.dbContext
                    .GlassCategories
                    .AsNoTracking()
                    .Select(c => new GlassSelectCategoryFormModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToArrayAsync();

            return allGlassCategory;
        }

        public async Task<bool> ExistsIdAsync(int id)
        {
            bool exists = await this.dbContext
                .GlassCategories
                .AnyAsync(g => g.Id == id);

            return exists;
        }
    }
}
