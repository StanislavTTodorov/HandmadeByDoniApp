

using HandmadeByDoniApp.Web.ViewModels.Box;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IBoxService
    {
        Task CreateBoxAsync(BoxFormModel formModel);

        Task<bool> ExistsByIdAsync(string boxId);

        Task<BoxDetailsViewModel> GetBoxDetailsByIdAsync(string boxId);
    }
}
