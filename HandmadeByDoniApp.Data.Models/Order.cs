using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeByDoniApp.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid();
            this.Products = new HashSet<Product>();
        }
        [Key]
        public Guid Id { get; set; }

        public ICollection<Product> Products { get; set; }

        [ForeignKey(nameof(User))]
        public Guid ClientId { get; set; }
        public ApplicationUser User { get; set; } = null!;


    }
}
