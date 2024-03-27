
using HandmadeByDoniApp.Web.ViewModels.Product;

namespace HandmadeByDoniApp.Web.ViewModels.Order
{
    public class MineProductViewModel
    {
        public MineProductViewModel()
        {
            this.Products=new HashSet<ProductsAllViewModel>();
        }

        public decimal totalPrice {  get; set; }

        public IEnumerable<ProductsAllViewModel> Products { get; set; }
    }
}
