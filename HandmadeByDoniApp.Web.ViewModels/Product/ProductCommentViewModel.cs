

using HandmadeByDoniApp.Web.ViewModels.Comment;

namespace HandmadeByDoniApp.Web.ViewModels.Product
{
    public class ProductCommentViewModel: ProductViewModel
    {
        public ProductCommentViewModel()
        {
            this.Comments = new HashSet<CommentViewModel>();
        }

       public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
