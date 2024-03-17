

using HandmadeByDoniApp.Web.ViewModels.Glass;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IGlassService
    {
        Task CreateGlassAsync(GlassFormModel formModel); 

        Task<bool> ExistsByIdAsync(string glassId);

        Task<GlassDetailsViewModel> GetGlassDetailsByIdAsync(string glassId);
    }
}
