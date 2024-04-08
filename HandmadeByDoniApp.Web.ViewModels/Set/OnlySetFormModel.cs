using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Set;
using HandmadeByDoniApp.Web.ViewModels.Product;

namespace HandmadeByDoniApp.Web.ViewModels.Set
{
    public class OnlySetFormModel
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

        public int NumberOfCups { get; set; }


        [Range(typeof(decimal), PriceMinLength, PriceMaxLength)]
        public decimal Price { get; set; }
    }
}
