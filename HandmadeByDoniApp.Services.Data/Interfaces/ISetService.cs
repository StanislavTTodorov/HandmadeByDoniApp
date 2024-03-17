

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface ISetService
    {
        Task<bool> ExistsByIdAsync(string setId);
    }
}
