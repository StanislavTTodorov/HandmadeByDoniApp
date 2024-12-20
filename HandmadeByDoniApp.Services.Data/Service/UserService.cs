﻿

using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;


namespace HandmadeByDoniApp.Services.Data.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<UserViewModel>> AllUsersAsync()
        {
            IEnumerable<UserViewModel> userViews = await this.repository
                .All<ApplicationUser>()
                .Select(x => new UserViewModel
                {
                    Id  =x.Id.ToString(),
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
    }
}
