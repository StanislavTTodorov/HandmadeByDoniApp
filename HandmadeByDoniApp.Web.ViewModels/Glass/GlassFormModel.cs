

using HandmadeByDoniApp.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Glass;

namespace HandmadeByDoniApp.Web.ViewModels.Glass
{
    public class GlassFormModel
    {
        public GlassFormModel()
        {
            this.Categories = new HashSet<GlassSelectCategoryFormModel>();
        }

        [Required]
        [StringLength(TitleMaxLength,MinimumLength =TitleMinLength)]
        public string Title { get; set; } = null!;

        [StringLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Capacity in Milliliters")]
        [Range(typeof(int), CapacityMinLength, CapacityMaxLength)]
        public int Capacity { get; set; }

        [Range(typeof(decimal), PriceMinLength,PriceMaxLength)]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Is in Set")]
        public bool IsSet { get; set; }

        public IEnumerable<GlassSelectCategoryFormModel> Categories { get; set;}

        public string? Id { get; set; }

    }
}
