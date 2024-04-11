using HandmadeByDoniApp.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Address;

namespace HandmadeByDoniApp.Web.ViewModels.Address
{
    public class AddressFormModel
    {
        public AddressFormModel()
        {
            this.DeliveryCompanies = new HashSet<DeliveryCompanyFormModel>();
            this.MethodPayments = new HashSet<MethodPaymentFormModel>();

        }

        [Required]
        [Display(Name = "Country Name")]
        [StringLength(CountryNameMaxLength, MinimumLength = CountryNameMinLength)]
        public string CountryName { get; set; } = null!;

        [Required]
        [Display(Name = "City Name")]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        public string CityName { get; set; } = null!;

        [Required]
        [Display(Name = "Street Name")]
        [StringLength(StreetMaxLength, MinimumLength = StreetMinLength)]
        public string Street { get; set; } = null!;

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Delivery Company")]
        public int DeliveryCompanyId { get; set; }
        public IEnumerable<DeliveryCompanyFormModel> DeliveryCompanies { get; set; }

        [Display(Name = "Method Payment")]
        public int MethodPaymentId { get; set; }
        public IEnumerable<MethodPaymentFormModel> MethodPayments { get; set; }


    }
}
