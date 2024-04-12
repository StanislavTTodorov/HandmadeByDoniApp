
using HandmadeByDoniApp.Web.ViewModels.User;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> AllUsersAsync();
        Task<string> GetFullNameByIdAsync(string userId);
    }
}
