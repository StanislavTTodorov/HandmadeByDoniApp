

using HandmadeByDoniApp.Web.ViewModels.Comment;

namespace HandmadeByDoniApp.Web.ViewModels.Product
{
    public class AllProductCommentViewModel: AllProductViewModel
    {
        public AllProductCommentViewModel()
        {
            this.Comments = new HashSet<CommentViewModel>();
        }

       public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
