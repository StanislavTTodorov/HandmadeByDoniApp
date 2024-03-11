

using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.GlassCategoty;

namespace HandmadeByDoniApp.Data.Models
{
    public class GlassCategory
    {
        public GlassCategory()
        {
            this.Glasses = new HashSet<Glass>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Glass> Glasses { get; set; }
    }
}
