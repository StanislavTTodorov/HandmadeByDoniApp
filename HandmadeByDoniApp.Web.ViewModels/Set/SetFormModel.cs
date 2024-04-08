

using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Set;
using HandmadeByDoniApp.Web.ViewModels.Product;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using HandmadeByDoniApp.Web.ViewModels.Decanter;


namespace HandmadeByDoniApp.Web.ViewModels.Set
{
    public class SetFormModel
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

        public DecanterFormModel? Decanter { get; set; }

        public GlassFormModel GlassOne { get; set; } = null!;

        public GlassFormModel GlassTwo { get; set; } = null!;

        public GlassFormModel? GlassThree { get; set; }

        public GlassFormModel? GlassFour { get; set; } 






    }
}
