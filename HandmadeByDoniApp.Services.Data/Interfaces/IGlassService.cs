

using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using HandmadeByDoniApp.Web.ViewModels.Product;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IGlassService
    {
        Task CreateGlassAsync(GlassFormModel formModel); 

        Task<bool> ExistsByIdAsync(string glassId);

        Task<GlassDetailsViewModel> GetGlassDetailsByIdAsync(string glassId);

        Task<AllProductCommentViewModel> GetGlassCommentByIdAsync(string glassId);

        Task CreateCommentByUserIdAndByProductIdAsync(string userId, CommentFormModel formModel, string productId);
        Task<GlassFormModel> GetGlassForEditByIdAsync(string id);
        Task EditGlassByIdAndFormModelAsync(string id, GlassFormModel formModel);
        Task SoftDeleteByIdAsync(string id);
        Task RecoveryByIdAsync(string id);
    }
}
