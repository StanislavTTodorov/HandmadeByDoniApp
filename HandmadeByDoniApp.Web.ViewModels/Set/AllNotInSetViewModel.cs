
using System.ComponentModel.DataAnnotations;

namespace HandmadeByDoniApp.Web.ViewModels.Set
{
    public class AllNotInSetViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
