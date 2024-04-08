

using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Order;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task AddProductByUserIdAsync(string userId, string id);
        Task<MineProductViewModel> AllMineProductsAsync(string userId);
        Task CreateRegisterOrderAsync(ApplicationUser user);
        Task<bool> ExistsInSetByIdAsync(string id);
        Task RemoveProductByUserIdAsync(string userId, string id);
    }
}
