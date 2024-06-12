using HandmadeByDoniApp.Web.ViewModels.Category;


namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<SelectCategoryFormModel>> AllCategoriesAsync();

        Task<bool> ExistsIdAsync(int id);

        Task<IEnumerable<string>> AllCategoryNameAsync();
    }
}
