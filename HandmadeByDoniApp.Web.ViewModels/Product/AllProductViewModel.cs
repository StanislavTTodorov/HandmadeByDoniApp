
using System.ComponentModel.DataAnnotations;

namespace HandmadeByDoniApp.Web.ViewModels.Product
{
    public class AllProductViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

    }
}
