
using System.ComponentModel.DataAnnotations;

namespace HandmadeByDoniApp.Web.ViewModels.Product
{
    public class ProductsAllViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        [Display(Name = "Create On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
