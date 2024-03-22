using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class GlassCategoryService : IGlassCategoryService
    {
        private readonly IRepository repository;

        public GlassCategoryService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<string>> AllCategoryNameAsync()
        {
            IEnumerable<string> allName = await this.repository.All<GlassCategory>()
                .Select (x => x.Name)
                .ToArrayAsync ();

            return allName;
        }

        public async Task<IEnumerable<GlassSelectCategoryFormModel>> AllGlassCategoriesAsync()
        {
            IEnumerable<GlassSelectCategoryFormModel> allGlassCategory =
            await this.repository
                    .AllReadOnly<GlassCategory>()
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
            bool exists = await this.repository
                .All<GlassCategory>()
                .AnyAsync(g => g.Id == id);

            return exists;
        }
    }
}
