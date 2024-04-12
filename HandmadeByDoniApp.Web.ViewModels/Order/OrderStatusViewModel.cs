using HandmadeByDoniApp.Web.ViewModels.Address;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandmadeByDoniApp.Web.ViewModels.Order
{
    public class OrderStatusViewModel
    {
        public string OrderId { get; set; } = null!;

        public OrderViewModel OrdersDetails{ get; set; } = null!;
    }
}
