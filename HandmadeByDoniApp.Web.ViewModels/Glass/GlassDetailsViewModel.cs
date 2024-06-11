using HandmadeByDoniApp.Web.ViewModels.Product;
using System.Collections;

namespace HandmadeByDoniApp.Web.ViewModels.Glass
{
    public class GlassDetailsViewModel : ProductViewModel
    {
        public int Capacity { get; set; }

        public string Category { get; set; } = null!;

        public bool IsSet { get; set; }

        public string? SetId { get; set; }

    }
}