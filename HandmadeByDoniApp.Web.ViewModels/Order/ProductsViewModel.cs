using HandmadeByDoniApp.Web.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace HandmadeByDoniApp.Web.ViewModels.Order
{
    public class ProductsViewModel
    {
        [Display(Name = "Total Price")]
        public decimal totalPrice { get; set; }

        public string OrderId { get; set; } = null!;

    }
}