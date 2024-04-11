

using HandmadeByDoniApp.Web.ViewModels.Address;

namespace HandmadeByDoniApp.Web.ViewModels.Order
{
    public class DetailsOrdarViewModel
    {
        public MineProductViewModel MineProduct { get; set; } = null!;

        public AddressFormModel? Address { get; set; }
    }
}
