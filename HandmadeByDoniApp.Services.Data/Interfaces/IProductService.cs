

using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Servises.Data.Models.Product;
using HandmadeByDoniApp.Web.ViewModels.Box;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using HandmadeByDoniApp.Web.ViewModels.Home;
using HandmadeByDoniApp.Web.ViewModels.Product;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<IndexViewModel>> LastTwelveProductsAsync();

        Task<AllProductFilteredAndPagedServiceModel> AllProductsAsync(AllProductsQueryModel queryModel);

        Task<bool> ExistsByIdAsync(string productId);

        Task CreateProductAsync(ProductFormModel formModel);

       // Task<GlassDetailsViewModel> GetProductDetailsByIdAsync(string glassId);

        // Task<AllProductCommentViewModel> GetProductCommentByIdAsync(string glassId);

        Task CreateCommentByUserIdAndByProductIdAsync(string userId, CommentFormModel formModel, string productId);

        Task<ProductFormModel> GetProductForEditByIdAsync(string id);

        Task EditProductByIdAndFormModelAsync(string id, ProductFormModel formModel);

        Task SoftDeleteByIdAsync(string id);

        Task RecoveryByIdAsync(string id);
        Task<ProductViewModel> GetProductDetailsByIdAsync(string id);
        Task<ProductCommentViewModel> GetProductCommentByIdAsync(string id);
    }
}
