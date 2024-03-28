
using HandmadeByDoniApp.Web.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace HandmadeByDoniApp.Web.ViewModels.Order
{
    public class MineProductViewModel
    {
        public MineProductViewModel()
        {
            this.Products=new HashSet<ProductsAllViewModel>();
        }
        [Display(Name = "Total Price")]
        public decimal totalPrice {  get; set; }

        public IEnumerable<ProductsAllViewModel> Products { get; set; }
    }
}
