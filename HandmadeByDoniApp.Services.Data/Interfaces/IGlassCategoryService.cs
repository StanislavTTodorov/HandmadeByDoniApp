
using HandmadeByDoniApp.Web.ViewModels.Category;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IGlassCategoryService
    {
        Task<IEnumerable<GlassSelectCategoryFormModel>> AllGlassCategoriesAsync();

        Task<bool> ExistsIdAsync(int id);

        Task<IEnumerable<string>> AllCategoryNameAsync();
    }
}
