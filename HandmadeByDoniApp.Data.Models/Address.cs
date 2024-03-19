using System.ComponentModel.DataAnnotations;
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





    }
}