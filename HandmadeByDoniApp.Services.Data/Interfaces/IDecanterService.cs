

using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Product;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IDecanterService
    {
        Task CreateDecanterAsync(DecanterFormModel formModel);

        Task<bool> ExistsByIdAsync(string decanterId);

        Task<DecanterDetailsViewModel> GetDecanterDetailsByIdAsync(string decanterId);

        Task<AllProductCommentViewModel> GetDecanterCommentByIdAsync(string glassId);

        Task CreateCommentByUserIdAndByProductIdAsync(string userId, CommentFormModel formModel, string productId);
    }
}
