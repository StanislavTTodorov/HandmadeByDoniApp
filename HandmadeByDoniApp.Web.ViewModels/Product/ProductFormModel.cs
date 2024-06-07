using HandmadeByDoniApp.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Product;

namespace HandmadeByDoniApp.Web.ViewModels.Product
{
    public class ProductFormModel
    {
        public ProductFormModel()
        {
            this.Categories = new HashSet<SelectCategoryFormModel>();
        }

        public string? Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [StringLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), PriceMinLength, PriceMaxLength)]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectCategoryFormModel> Categories { get; set; }
    }
}
