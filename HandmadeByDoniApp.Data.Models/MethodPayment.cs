
using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.MethodPayment;

namespace HandmadeByDoniApp.Data.Models
{
    public class MethodPayment
    {
 
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MethodMaxLength)]
        public string Method { get; set; } = null!;
    }
}