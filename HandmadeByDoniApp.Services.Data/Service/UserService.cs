

using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
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

        public async Task AddProductByUserIdAsync(string userId, string productId)
        {
            ApplicationUser? user = await this.repository.All<ApplicationUser>().FirstAsync(u => u.Id.ToString() == userId);
            if (user != null)
            {
                if(await this.repository.All<Box>().AnyAsync(b=>b.Id.ToString()==productId))
                {
                    Box box = await this.repository.All<Box>().Where(b=>b.Id.ToString()==productId).FirstAsync();
                    user.Boxs.Add(box);
                    
                }
                else if(await this.repository.All<Glass>().AnyAsync(g => g.Id.ToString() == productId))
                {
                    Glass glass = await this.repository.All<Glass>().Where(g => g.Id.ToString() == productId).FirstAsync();
                    user.Glasses.Add(glass);

                }
                else if (await this.repository.All<Set>().AnyAsync(g => g.Id.ToString() == productId))
                {
                    Set set = await this.repository.All<Set>().Where(g => g.Id.ToString() == productId).FirstAsync();
                    user.Sets.Add(set);

                }
                else if (await this.repository.All<Decanter>().AnyAsync(g => g.Id.ToString() == productId))
                {
                    Decanter decanter = await this.repository.All<Decanter>().Where(g => g.Id.ToString() == productId).FirstAsync();
                    user.Decanters.Add(decanter);

                }
                await this.repository.SaveChangesAsync();
            }

            
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
