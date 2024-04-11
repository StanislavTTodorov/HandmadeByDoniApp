using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Address;

namespace HandmadeByDoniApp.Data.Models
{
    public class Address
    {
        public Address()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }


        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string CountryName { get; set; } = null!;

        [Required]
        [MaxLength(CityNameMaxLength)]
        public string CityName { get; set; } = null!;

        [Required]
        [MaxLength(StreetMaxLength)]
        public string Street { get; set; } = null!;

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(DeliveryCompany))]
        public int DeliveryCompanyId { get; set; }
        public virtual DeliveryCompany DeliveryCompany { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(MethodPayment))]
        public int MethodPaymentId { get; set; }
        public virtual MethodPayment MethodPayment { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public Guid ClientId { get; set; }
        public ApplicationUser User { get; set; } = null!;

    }
}