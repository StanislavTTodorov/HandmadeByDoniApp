

using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.DeliveryCompany;

namespace HandmadeByDoniApp.Data.Models
{
    public class DeliveryCompany
    {
      
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
