

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
        Task<ICollection<AllNotInSetViewModel>> GetGlassesInSetAsync();
        Task<ICollection<AllNotInSetViewModel>> GetDecantersInSetAsync();
    }
}
