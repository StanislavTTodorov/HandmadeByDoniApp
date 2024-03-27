

using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Order;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task<MineProductViewModel> AllMineProductsAsync(string userId);
        Task CreateRegisterOrderAsync(ApplicationUser user);
    }
}
