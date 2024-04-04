

using HandmadeByDoniApp.Web.ViewModels.Box;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Product;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IBoxService
    {
        Task CreateBoxAsync(BoxFormModel formModel);

        Task<bool> ExistsByIdAsync(string boxId);

        Task<BoxDetailsViewModel> GetBoxDetailsByIdAsync(string boxId);

        Task<AllProductCommentViewModel> GetBoxCommentByIdAsync(string glassId);

        Task CreateCommentByUserIdAndByProductIdAsync(string userId, CommentFormModel formModel, string productId);
        Task<BoxFormModel> GetBoxForEditByIdAsync(string id);
        Task EditBoxByIdAndFormModelAsync(string id, BoxFormModel formModel);
    }
}
