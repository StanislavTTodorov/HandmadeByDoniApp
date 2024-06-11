using HandmadeByDoniApp.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandmadeByDoniApp.Web.ViewModels.Set
{
    public class SetDetailsViewModel: ProductViewModel
    {
        public SetDetailsViewModel()
        {
            this.SetProducts = new HashSet<ProductViewModel>();
        }
        public ICollection<ProductViewModel> SetProducts { get; set; }

        
    }
}
