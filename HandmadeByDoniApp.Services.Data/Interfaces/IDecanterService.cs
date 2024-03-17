

using HandmadeByDoniApp.Web.ViewModels.Decanter;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IDecanterService
    {
        Task CreateDecanterAsync(DecanterFormModel formModel);

        Task<bool> ExistsByIdAsync(string decanterId);

        Task<DecanterDetailsViewModel> GetDecanterDetailsByIdAsync(string decanterId);
    }
}
