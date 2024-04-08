using HandmadeByDoniApp.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandmadeByDoniApp.Web.ViewModels.Set
{
    public class SetDetailsViewModel: AllProductViewModel
    {
        public SetDetailsViewModel()
        {
            this.SetProducts = new HashSet<AllProductViewModel>();
        }
        public ICollection<AllProductViewModel> SetProducts { get; set; }

        
    }
}
