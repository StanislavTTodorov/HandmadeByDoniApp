
using HandmadeByDoniApp.Web.ViewModels.Order;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task AddProductByUserIdAsync(string userId, string id);

        Task<MineProductViewModel> AllMineProductsByUserIdAsync(string userId);

        Task<MineProductViewModel> AllOrderProductsByOrderIdAsync(string id);

        Task<bool> CreateRegisterUserOrderByUserIdAsync(string userId);

        Task EditSentToTrueAsync(string orderId , string? ShipmentNoteNumber = null);

        Task<bool> UserOrderExistsByOrderIdAsync(string orderId);

        Task<bool> UserOrderExistsByUserIdAsync(string userId);

        Task<ICollection<AdminOrdersViewModel>> GetUserOrdersAsync();

        Task<ICollection<OrderStatusViewModel>> GetUserOrdersByUserIdAsync(string userId);

        Task<EditOrderViewModel> GetUserOrderByOrdeIdAsync(string orderId);

        Task<bool> IsActiveByIdAsync(string id);

		Task RemoveProductByUserIdAsync(string userId, string id);

        Task DeleteUserOrderByOrderIdAsync(string orderId);

        Task<bool> UserOrderIsSentByOrderIdAsync(string id);
    }
}
