

using HandmadeByDoniApp.Web.ViewModels.Box;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IBoxService
    {
        Task CreateBoxAsync(BoxFormModel formModel);
    }
}
