using HandmadeByDoniApp.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Product;

namespace HandmadeByDoniApp.Web.ViewModels.Product
{
    public class ProductFormModel
    {
        public ProductFormModel()
        {
            this.Categories = new HashSet<SelectCategoryFormModel>();
            this.Images = new HashSet<IFormFile>();
        }

        public string? Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [StringLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        public string? ImageUrls { get; set; }

        [Required(ErrorMessage = "Please Upload Files")]
        [Display(Name = "Upload Files")]
        public IEnumerable<IFormFile> Images { get; set; } 

        [Range(typeof(decimal), PriceMinLength, PriceMaxLength)]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectCategoryFormModel> Categories { get; set; }
    }
}
