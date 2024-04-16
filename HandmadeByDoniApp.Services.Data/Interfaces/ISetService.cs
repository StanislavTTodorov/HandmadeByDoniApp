

using HandmadeByDoniApp.Web.ViewModels.Comment;

using HandmadeByDoniApp.Web.ViewModels.Product;
using HandmadeByDoniApp.Web.ViewModels.Set;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface ISetService
    {
        Task<bool> ExistsByIdAsync(string setId);

        Task<AllProductCommentViewModel> GetSetCommentByIdAsync(string setId);

        Task CreateCommentByUserIdAndByProductIdAsync(string userId, CommentFormModel formModel, string productId);
        
        Task CreateSetAsync(SetFormModel model);
        Task<SetDetailsViewModel> GetSetDetailsByIdAsync(string id);
        Task<SetFormModel> GetSetForEditByIdAsync(string id);
        Task EditSetByIdAndFormModelAsync(string id, OnlySetFormModel formModel);
        Task SoftDeleteByIdAsync(string id);
        Task RecoveryByIdAsync(string id);
    }
}
