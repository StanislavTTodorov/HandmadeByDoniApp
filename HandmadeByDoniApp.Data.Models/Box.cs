
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Box;

namespace HandmadeByDoniApp.Data.Models
{
    public class Box
    {
        public Box()
        {
            this.Id = Guid.NewGuid();
            this.Comments = new HashSet<Comment>();
            this.ApplicationUsers = new HashSet<ApplicationUser>();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid? OrderId { get; set; }
        public virtual Order? Order { get; set; }

       
    }
}
