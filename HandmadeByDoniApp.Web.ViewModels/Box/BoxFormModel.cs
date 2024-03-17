

using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Box;

namespace HandmadeByDoniApp.Web.ViewModels.Box
{
    public class BoxFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [StringLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Range(CapacityMin,CapacityMax)]
        [Display(Name = "Number of cups inside")]
        public int Capacity { get; set; }

        [Range(typeof(int), PriceMinLength, PriceMaxLength)]
        public decimal Price { get; set; }
    }
}
