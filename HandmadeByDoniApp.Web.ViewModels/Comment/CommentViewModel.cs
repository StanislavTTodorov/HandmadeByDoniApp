

namespace HandmadeByDoniApp.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            this.Comments = new HashSet<CommentViewModel>();
        }
        public string Id { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Text { get; set; } = null!;

        public string Time { get; set; } = null!;

         public IEnumerable<CommentViewModel> Comments { get; set; }

    }
}
