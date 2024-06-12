
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

        [ForeignKey(nameof(OnComment))]
        public Guid? CommentId { get; set; }
        public virtual Comment? OnComment { get; set; }

        //[ForeignKey(nameof(Box))]
        //public Guid? BoxId { get; set; }
        //public virtual Box? Box { get; set; }

        //[ForeignKey(nameof(Glass))]
        //public Guid? GlassId { get; set; }
        //public virtual Glass? Glass { get; set; }

        //[ForeignKey(nameof(Set))]
        //public Guid? SetId { get; set; }
        //public virtual Set? Set { get; set; }

        //[ForeignKey(nameof(Decanter))]
        //public Guid? DecanterId { get; set; }
        //public virtual Decanter? Decanter { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
