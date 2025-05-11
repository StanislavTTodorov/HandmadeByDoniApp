
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using static HandmadeByDoniApp.Common.EntityValidationConstants;


namespace HandmadeByDoniApp.Services.Data.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        private readonly IOrderService orderService;

        public UserService(IRepository repository, IOrderService orderService)
        {
            this.repository = repository;
            this.orderService = orderService;

        }

        public async Task<IEnumerable<UserViewModel>> AllUsersAsync()
        {
            IEnumerable<UserViewModel> userViews = await this.repository
                .All<ApplicationUser>()
                .Select(x => new UserViewModel
                {
                    Id = x.Id.ToString(),
                    Email = x.Email,
                    FullName = $"{x.FirstName} {x.LastName}",

                }).ToArrayAsync();

            return userViews;
        }


        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            ApplicationUser? user = await this.repository
                .All<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<bool> IsUserHasOrderAsync(string userId)
        {         
            bool isExists = await this.orderService.UserOrderExistsByUserIdAsync(userId);
            return isExists;
        }
    }
}
