
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Comment;



namespace HandmadeByDoniApp.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid();
            this.Comments = new HashSet<Comment>();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } =  null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
