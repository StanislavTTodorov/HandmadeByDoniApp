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
    }
}
