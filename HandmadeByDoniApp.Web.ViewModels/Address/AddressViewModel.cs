

using System.ComponentModel.DataAnnotations;

namespace HandmadeByDoniApp.Web.ViewModels.Address
{
    public class AddressViewModel
    {
        [Display(Name = "Country Name")]
        public string CountryName { get; set; } = null!;

        [Display(Name = "City Name")]
        public string CityName { get; set; } = null!;


        [Display(Name = "Street Name")]
        public string Street { get; set; } = null!;

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Delivery Company")]
        public string DeliveryCompanyName { get; set; } = null!;

        [Display(Name = "Method Payment")]
        public string MethodPayment { get; set; } = null!;
    }

}
