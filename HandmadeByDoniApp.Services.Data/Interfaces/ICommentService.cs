

using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Product;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentToCommentByUserIdAndByCommentIdAsync(string userId, CommentFormModel formModel, string commentId);

        Task EditCommentByIdAndFormModelAsync(string commentId, CommentFormModel formModel);

        Task<bool> ExistsByIdAsync(string Id);

        Task<CommentFormModel> GetCommentForEditByIdAsync(string commentId);

        Task<bool> HasUserCommentByUserIdAndByCommentIdAsync(string userId, string commentId);

    }
}
