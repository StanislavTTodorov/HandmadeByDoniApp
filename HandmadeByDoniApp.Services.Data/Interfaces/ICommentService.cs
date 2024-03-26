

using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Product;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentToCommentByUserIdAndByCommentIdAsync(string userId, CommentFormModel formModel, string commentId);

        Task<bool> ExistsByIdAsync(string Id);

    }
}
