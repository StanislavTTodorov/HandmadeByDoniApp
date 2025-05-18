

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeByDoniApp.Data.Models
{
    public class UserOrder
    {
        public UserOrder()
        {
           
        }     

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        [Required] 
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; } 
        public Order Order { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime CreaateOn { get; set; }

        [Required]
        [ForeignKey(nameof(Address))]
        public Guid AddressId { get; set; }
        public Address Address {  get; set; } = null!;

        public bool IsSent { get; set; }

        public string? ShipmentNoteNumber { get; set; }


    }
}
