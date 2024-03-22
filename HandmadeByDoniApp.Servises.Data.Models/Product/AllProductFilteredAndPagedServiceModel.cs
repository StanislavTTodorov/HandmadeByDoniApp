

using HandmadeByDoniApp.Web.ViewModels.Product;
using System.Runtime.CompilerServices;

namespace HandmadeByDoniApp.Servises.Data.Models.Product
{
    public class AllProductFilteredAndPagedServiceModel
    {
        public AllProductFilteredAndPagedServiceModel()
        {
            this.Products = new HashSet<ProductsAllViewModel>();
        }
        public int  TotalProductCount { get; set; }

        public IEnumerable<ProductsAllViewModel> Products { get; set; }
    }
}
 