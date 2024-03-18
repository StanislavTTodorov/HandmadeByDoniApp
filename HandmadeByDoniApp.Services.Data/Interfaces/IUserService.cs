

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<string> GetFullNameByIdAsync(string userId);
    }
}
