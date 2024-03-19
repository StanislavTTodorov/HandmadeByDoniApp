

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Set;

namespace HandmadeByDoniApp.Data.Models
{
    public class Set
    {
        public Set()
        {
            this.Id = Guid.NewGuid();
            this.Glasss = new HashSet<Glass>();
            this.Comments = new HashSet<Comment>();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }= null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreateOn { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Glass> Glasss { get; set; }

        [ForeignKey(nameof(Decanter))]
        public Guid? DecanterId { get; set; }
        public virtual Decanter? Decanter { get; set; }

        public bool IsActive { get; set; }
    }
}
