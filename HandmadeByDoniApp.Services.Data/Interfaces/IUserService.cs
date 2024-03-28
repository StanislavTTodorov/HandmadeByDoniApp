

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task AddProductByUserIdAsync(string userId, string productId);
        Task<string> GetFullNameByIdAsync(string userId);


    }
}
