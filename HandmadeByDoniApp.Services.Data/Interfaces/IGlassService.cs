

using HandmadeByDoniApp.Web.ViewModels.Glass;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IGlassService
    {
        Task CreateGlassAsync(GlassFormModel formModel); 
    }
}
