using HandmadeByDoniApp.Web.ViewModels.Address;

namespace HandmadeByDoniApp.Web.ViewModels.Order
{
    public class AdminOrdersViewModel:AddressViewModel
    {
        public string OrderId { get; set; } = null!;

        public string AddressId { get; set; } = null!;

        public string Data { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string TotalPrice {  get; set; } = null!;

        public bool IsSent { get; set; }
    }
}