using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Order;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;

        public OrderService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<MineProductViewModel> AllMineProductsAsync(string userId)
        {
            ApplicationUser? user = await this.repository
                .All<ApplicationUser>()
                .Include(u=>u.Boxs)
                .Include(u => u.Sets)
                .Include(u => u.Glasses)
                .Include(u => u.Decanters)
                .FirstAsync(u => u.Id.ToString() == userId);
            List<ProductsAllViewModel> products = new List<ProductsAllViewModel>();
            if (user != null)
            {
                List<ProductsAllViewModel> boxs = user.Boxs.Select(x => new ProductsAllViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    Price = x.Price,
                    IsActive = x.IsActive,
                    ImageUrl = x.ImageUrl
                }).ToList();
                List<ProductsAllViewModel> glasses = user.Glasses.Select(x => new ProductsAllViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    Price = x.Price,
                    IsActive = x.IsActive,
                    ImageUrl = x.ImageUrl
                }).ToList();
                List<ProductsAllViewModel> sets = user.Sets.Select(x => new ProductsAllViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    Price = x.Price,
                    IsActive = x.IsActive,
                    ImageUrl = x.ImageUrl
                }).ToList();
                List<ProductsAllViewModel> decanters = user.Decanters.Select(x => new ProductsAllViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    Price = x.Price,
                    IsActive = x.IsActive,
                    ImageUrl = x.ImageUrl
                }).ToList();
                products.AddRange(boxs);
                products.AddRange(glasses);
                products.AddRange(sets);
                products.AddRange(decanters);
            }
            MineProductViewModel viewModel = new MineProductViewModel()
            {
                totalPrice = products.Sum(x => x.Price),
                Products = products
            };

            return viewModel;
        }

        public  async Task CreateRegisterOrderAsync(ApplicationUser user)
        {
            Order newOrder = new Order()
            {
                User = user
            };

            await repository.AddAsync(newOrder);
            await repository.SaveChangesAsync();
        }

        public async Task AddProductByUserIdAsync(string userId, string productId)
        {
            ApplicationUser? user = await this.repository.All<ApplicationUser>().FirstAsync(u => u.Id.ToString() == userId);
            if (user != null)
            {
                if (await this.repository.All<Box>().AnyAsync(b => b.Id.ToString() == productId))
                {
                    Box box = await this.repository.All<Box>().Where(b => b.Id.ToString() == productId).FirstAsync();
                    user.Boxs.Add(box);

                }
                else if (await this.repository.All<Glass>().AnyAsync(g => g.Id.ToString() == productId))
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

        public async Task RemoveProductByUserIdAsync(string userId, string productId)
        {
            ApplicationUser? user = await this.repository
                .All<ApplicationUser>()
                .Include(u => u.Boxs)
                .Include(u => u.Sets)
                .Include(u => u.Glasses)
                .Include(u => u.Decanters)
                .FirstAsync(u => u.Id.ToString() == userId);
            if (user != null)
            {
                if (await this.repository.All<Box>().AnyAsync(b => b.Id.ToString() == productId) &&
                    user.Boxs.Any(b => b.Id.ToString() == productId))
                {
                    Box box = await this.repository.All<Box>().Where(b => b.Id.ToString() == productId).FirstAsync();
                    user.Boxs.Remove(box);
                }
                else if (await this.repository.All<Glass>().AnyAsync(b => b.Id.ToString() == productId) &&
                    user.Glasses.Any(b => b.Id.ToString() == productId))
                {
                    Glass glass = await this.repository.All<Glass>().Where(b => b.Id.ToString() == productId).FirstAsync();
                    user.Glasses.Remove(glass);
                }
                else if (await this.repository.All<Set>().AnyAsync(b => b.Id.ToString() == productId) &&
                    user.Sets.Any(b => b.Id.ToString() == productId))
                {
                    Set set = await this.repository.All<Set>().Where(b => b.Id.ToString() == productId).FirstAsync();
                    user.Sets.Remove(set);
                }
                else if (await this.repository.All<Decanter>().AnyAsync(b => b.Id.ToString() == productId) &&
                    user.Decanters.Any(b => b.Id.ToString() == productId))
                {
                    Decanter decanter = await this.repository.All<Decanter>().Where(b => b.Id.ToString() == productId).FirstAsync();
                    user.Decanters.Remove(decanter);
                }

                await this.repository.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsInSetByIdAsync(string id)
        {
            Glass? glass = await this.repository
                .All<Glass>()
                .FirstOrDefaultAsync(g=>g.Id.ToString()==id);
            Decanter? decanter =  await this.repository
                .All<Decanter>()
                .FirstOrDefaultAsync(g => g.Id.ToString() == id);
            
            if(glass!=null && glass.SetId!=null)
            {
                return true;
            }
            if (decanter != null && decanter.SetId != null)
            {
                return true;
            }
            return false;

        }
    }
}
