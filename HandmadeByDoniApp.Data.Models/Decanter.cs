

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Decanter;

namespace HandmadeByDoniApp.Data.Models
{
    public class Decanter
    {
        public Decanter()
        {
            this.Id = Guid.NewGuid();
            this.Comments = new HashSet<Comment>();
            this.ApplicationUsers = new HashSet<ApplicationUser>();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int Capacity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public bool IsSet { get; set; }

        [ForeignKey(nameof(Set))]
        public Guid? SetId { get; set; }
        public virtual Set? Set { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid? OrderId { get; set; }
        public virtual Order? Order { get; set; }

    }
}
