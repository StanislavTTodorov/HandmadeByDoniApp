
using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Comment;


namespace HandmadeByDoniApp.Web.ViewModels.Comment
{
    public class CommentFormModel
    {
        [Required]
        [StringLength(TextMaxLength,MinimumLength =TextMinLength)]
        public string Text { get; set; } = null!;
    }
}
