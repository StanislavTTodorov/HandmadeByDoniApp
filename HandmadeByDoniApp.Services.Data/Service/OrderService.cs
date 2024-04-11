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

        public async Task<MineProductViewModel> AllMineProductsByUserIdAsync(string userId)
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

        public  async Task<bool> CreateRegisterOrderAsync(string userId)
        {
            ApplicationUser user = await this.repository
                .All<ApplicationUser>()
                .Include(u=>u.Sets)
                .Include(u => u.Glasses)
                .Include(u => u.Decanters)
                .Include(u => u.Boxs)
                .FirstAsync(u=>u.Id.ToString()==userId);

            Order newOrder = new Order()
            {
                User = user,
                ClientId= user.Id,
                Sets = new HashSet<Set>(),
                Glasses = new HashSet<Glass>(),
                Decanters = new HashSet<Decanter>(),
                Boxs = new HashSet<Box>(),
            };
            //Glass
            Glass[] newGlass = new Glass[user.Glasses.Count];
               
            user.Glasses.CopyTo(newGlass, 0);
            foreach (var glass in user.Glasses)
            {
                if(glass.IsActive==false)
                {
                  return false;
                }
                else
                {
                    glass.IsActive = false;
                }
            }
            user.Glasses.Clear();
            newOrder.Glasses = newGlass;
            //Box
            Box[] newBox = new Box[user.Boxs.Count];

            user.Boxs.CopyTo(newBox, 0);
            foreach (var box in user.Boxs)
            {
                if (box.IsActive == false)
                {
                    return false;
                }
                else
                {
                    box.IsActive = false;
                }
            }
            user.Boxs.Clear();
            newOrder.Boxs = newBox;
            //Decanter
            Decanter[] newDecanter = new Decanter[user.Decanters.Count];

            user.Decanters.CopyTo(newDecanter, 0);
            foreach (var decanter in user.Decanters)
            {
                if (decanter.IsActive == false)
                {
                    return false;
                }
                else
                {
                    decanter.IsActive = false;
                }
            }
            user.Decanters.Clear();
            newOrder.Decanters = newDecanter;
            //Set
            Set[] newSet = new Set[user.Sets.Count];

            user.Sets.CopyTo(newSet, 0);
            foreach (var set in user.Sets)
            {
                if (set.IsActive == false)
                {
                    return false;
                }
                else
                {
                    set.IsActive = false;
                }
            }
            user.Sets.Clear();
            newOrder.Sets = newSet;

            await repository.AddAsync(newOrder);


            await repository.SaveChangesAsync();
            return true;
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

		public async Task<bool> IsActiveByIdAsync(string id)
		{
			bool glass = await this.repository.AllReadOnly<Glass>()
                .Where(g=>g.IsActive==true)
				.AnyAsync(g => g.Id.ToString() == id);

			bool box = await this.repository.AllReadOnly<Box>()
				.Where(g => g.IsActive == true)
				.AnyAsync(g => g.Id.ToString() == id);

			bool decanter = await this.repository.AllReadOnly<Decanter>()
				.Where(g => g.IsActive == true)
				.AnyAsync(g => g.Id.ToString() == id);

			bool set = await this.repository.AllReadOnly<Set>()
				.Where(g => g.IsActive == true)
				.AnyAsync(g => g.Id.ToString() == id);

			return glass||box||decanter||set;
		}
	}
}
