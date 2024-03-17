
using HandmadeByDoniApp.Web.ViewModels.Product;

namespace HandmadeByDoniApp.Web.ViewModels.Decanter
{
    public class DecanterDetailsViewModel:AllProductViewModel
    {      
        public int Capacity { get; set; }

        public bool IsSet { get; set; }
    }
}
