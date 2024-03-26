

using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Product;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface ISetService
    {
        Task<bool> ExistsByIdAsync(string setId);

        Task<AllProductCommentViewModel> GetSetCommentByIdAsync(string setId);

        Task CreateCommentByUserIdAndByProductIdAsync(string userId, CommentFormModel formModel, string productId);
    }
}
