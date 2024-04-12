

using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Order;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task AddProductByUserIdAsync(string userId, string id);
        Task<MineProductViewModel> AllMineProductsByUserIdAsync(string userId);
        Task<MineProductViewModel> AllOrderProductsByUserIdAsync(string userId, string orderId);
        Task<bool> CreateRegisterUserOrderByUserIdAsync(string userId);
        Task<bool> ExistsByUserIdAsync(string userId);
        Task<bool> ExistsInSetByIdAsync(string id);
        //Task<MineProductViewModel> GetOrderProductsByOrderIdAsync(string orderId);
       // Task<ICollection<OrderViewModel>> GetUserOrderByUserIdAsync(string userId);
        Task<ICollection<OrderStatusViewModel>> GetUserOrdersByUserIdAsync(string userId);
        Task<bool> IsActiveByIdAsync(string id);
		Task RemoveProductByUserIdAsync(string userId, string id);
    }
}
