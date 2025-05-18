
using HandmadeByDoniApp.Web.ViewModels.Address;
using System.ComponentModel.DataAnnotations;

namespace HandmadeByDoniApp.Web.ViewModels.Order
{
    public class OrderViewModel:AddressViewModel
    {
        public string OrderId { get; set; } = null!;

        public bool IsSent { get; set; }

        public string CreaateOn { get; set; } = null!;

        public string TotalPrice { get; set; } = null!;

        public string ShipmentNoteNumber { get; set; } = null!;
    }

    public class EditOrderViewModel
    {
        public string OrderId { get; set; } = null!;

       // public bool IsSent { get; set; }

        public string ShipmentNoteNumber { get; set; } = null!;

        //public string UserName { get; set; } = null!;

        //public string UserEmail { get; set; } = null!;

    }
}
