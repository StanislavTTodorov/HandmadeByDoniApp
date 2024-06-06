

using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class CategoryService
    {
        private readonly IRepository repository;

        public CategoryService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<string>> AllCategoryNameAsync()
        {
            IEnumerable<string> allName = await this.repository.All<Category>()
                .Select(x => x.Name)
                .ToArrayAsync();

            return allName;
        }

        public async Task<IEnumerable<SelectCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<SelectCategoryFormModel> allCategory =
            await this.repository
                    .AllReadOnly<Category>()
                    .Select(c => new SelectCategoryFormModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToArrayAsync();

            return allCategory;
        }

        public async Task<bool> ExistsIdAsync(int id)
        {
            bool exists = await this.repository
                .All<Category>()
                .AnyAsync(g => g.Id == id);

            return exists;
        }
    }
}
