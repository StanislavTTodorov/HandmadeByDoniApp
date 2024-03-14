

using HandmadeByDoniApp.Web.ViewModels.Home;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<IndexViewModel>> LastTwelveProductsAsync();
    }
}
