

using HandmadeByDoniApp.Web.ViewModels.Product.Enums;
using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;

namespace HandmadeByDoniApp.Web.ViewModels.Product
{
    public class AllProductsQueryModel
    {
        public AllProductsQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.ProductPerPage =DefaultEntitiesPerPage;

            this.GlassCategories = new HashSet<string>();
            this.Products = new HashSet<ProductsAllViewModel>();
        }
        [Display(Name = "Products")]
        public ProductsName ProductsName { get; set; }

        [Display(Name = "Glass Category")]
        public string? GlassCategory { get; set;}

        [Display(Name = "Search by word")]
        public string? SearchString {  get; set; }

        [Display(Name = "Sort products by")]
        public ProductSorting ProductSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Product Per Page")]
        public int ProductPerPage { get; set; }

        public int TotalProduct { get; set; }

        public IEnumerable<string> GlassCategories { get; set; } = null!;

        public IEnumerable<ProductsAllViewModel> Products{ get; set; }
    }
}
