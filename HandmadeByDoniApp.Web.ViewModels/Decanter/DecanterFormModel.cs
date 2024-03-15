
using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Decanter;
namespace HandmadeByDoniApp.Web.ViewModels.Decanter
{
    public class DecanterFormModel
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

        [Display(Name = "Capacity in Milliliters")]
        public int Capacity { get; set; }

        public decimal Price { get; set; }


        [Display(Name = "Is in Set")]
        public bool IsSet { get; set; }
    }
}
