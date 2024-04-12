using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Order;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


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
                .Include(u => u.Boxs)
                .Include(u => u.Sets)
                .Include(u => u.Glasses)
                .Include(u => u.Decanters)
                .FirstAsync(u => u.Id.ToString() == userId);
            List<ProductsAllViewModel> products = new List<ProductsAllViewModel>();
            if (user != null)
            {
                products = GetAllMineProduct(user.Boxs, user.Glasses, user.Sets, user.Decanters);

            }
            MineProductViewModel viewModel = new MineProductViewModel()
            {
                totalPrice = products.Sum(x => x.Price),
                Products = products
            };

            return viewModel;
        }

        private List<ProductsAllViewModel> GetAllMineProduct(ICollection<Box> mineBoxs, ICollection<Glass> mineGlasses, ICollection<Set> mineSets, ICollection<Decanter> mineDecanters)
        {
            List<ProductsAllViewModel> products = new List<ProductsAllViewModel>();
            List<ProductsAllViewModel> boxs = mineBoxs.Select(x => new ProductsAllViewModel()
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                Price = x.Price,
                IsActive = x.IsActive,
                ImageUrl = x.ImageUrl
            }).ToList();
            List<ProductsAllViewModel> glasses = mineGlasses.Select(x => new ProductsAllViewModel()
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                Price = x.Price,
                IsActive = x.IsActive,
                ImageUrl = x.ImageUrl
            }).ToList();
            List<ProductsAllViewModel> sets = mineSets.Select(x => new ProductsAllViewModel()
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                Price = x.Price,
                IsActive = x.IsActive,
                ImageUrl = x.ImageUrl
            }).ToList();
            List<ProductsAllViewModel> decanters = mineDecanters.Select(x => new ProductsAllViewModel()
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
            return products;
        }

        public async Task<bool> CreateRegisterUserOrderByUserIdAsync(string userId)
        {
            ApplicationUser user = await this.repository
                .All<ApplicationUser>()
                .Include(u => u.Sets)
                .Include(u => u.Glasses)
                .Include(u => u.Decanters)
                .Include(u => u.Boxs)
                .FirstAsync(u => u.Id.ToString() == userId);

            Order newOrder = new Order()
            {
                User = user,
                ClientId = user.Id,
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
                if (glass.IsActive == false)
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

            Address address = await this.repository
                              .All<Address>()
                              .Include(a => a.DeliveryCompany)
                              .Include(a => a.MethodPayment)
                              .FirstAsync();

            UserOrder userOrder = new UserOrder()
            {
                User = user,
                UserId = user.Id,
                OrderId = newOrder.Id,
                Order = newOrder,
                TotalPrice = GetAllMineProduct(newOrder.Boxs, newOrder.Glasses, newOrder.Sets, newOrder.Decanters).Sum(x => x.Price),
                CreaateOn = DateTime.Now,
                Address = address,
                AddressId = address.Id,
                IsSent = false,
            };
            await repository.AddAsync(userOrder);
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
                .FirstOrDefaultAsync(g => g.Id.ToString() == id);
            Decanter? decanter = await this.repository
                .All<Decanter>()
                .FirstOrDefaultAsync(g => g.Id.ToString() == id);

            if (glass != null && glass.SetId != null)
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
                .Where(g => g.IsActive == true)
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

            return glass || box || decanter || set;
        }


        public async Task<bool> ExistsByUserIdAsync(string userId)
        {
            bool exists = await this.repository.AllReadOnly<UserOrder>().AnyAsync(u => u.UserId.ToString() == userId);
            return exists;
        }

        public async Task<ICollection<OrderStatusViewModel>> GetUserOrdersByUserIdAsync(string userId)
        {
            ICollection<OrderStatusViewModel> orders = await this.repository
                .All<UserOrder>()
                .Include(u => u.User)
                .Include(u => u.Order)
                .Include(a => a.Address)
                .Where(u => u.UserId.ToString() == userId)
                .OrderBy(u => u.CreaateOn)
                .Select(u => new OrderStatusViewModel()
                {
                    OrderId = u.Order.Id.ToString(),

                    OrdersDetails = new OrderViewModel()
                    {
                        CountryName = u.Address.CountryName,
                        CityName = u.Address.CityName,
                        Street = u.Address.Street,
                        PhoneNumber = u.Address.PhoneNumber,
                        DeliveryCompanyName = u.Address.DeliveryCompany.Name,
                        MethodPayment = u.Address.MethodPayment.Method,
                        CreaateOn = u.CreaateOn.ToString("dd/MM/yyyy HH:mm"),
                        TotalPrice = u.TotalPrice.ToString("f2"),
                        IsSent = u.IsSent
                    }
                })
                .ToArrayAsync();

            return orders;

        }

        public async Task<MineProductViewModel> AllOrderProductsByOrderIdAsync(string orderId)
        {
            Order? order = await this.repository
                 .All<Order>()
                 .Include(u => u.Boxs)
                 .Include(u => u.Sets)
                 .Include(u => u.Glasses)
                 .Include(u => u.Decanters)
                 .Where(u => u.Id.ToString() == orderId)
                 .FirstOrDefaultAsync();
            List<ProductsAllViewModel> products = new List<ProductsAllViewModel>();
            if (order != null)
            {
                products = GetAllMineProduct(order.Boxs, order.Glasses, order.Sets, order.Decanters);

            }
            MineProductViewModel viewModel = new MineProductViewModel()
            {
                totalPrice = products.Sum(x => x.Price),
                Products = products
            };
            return viewModel;
        }

        public  async Task<ICollection<AdminOrdersViewModel>> GetUserOrdersAsync()
        {
            ICollection<AdminOrdersViewModel> orders = await this.repository
                .All<UserOrder>()
                .Include(u => u.User)
                .Include(u => u.Order)
                .Include(a => a.Address)
                .OrderBy(u => u.IsSent)
                .ThenByDescending(u => u.CreaateOn)
                .Select(u => new AdminOrdersViewModel()
                {
                    UserId = u.UserId.ToString(),
                    Email = u.User.Email,
                    FullName = $"{u.User.FirstName} {u.User.LastName}",

                    OrderId = u.OrderId.ToString(),
                    AddressId = u.AddressId.ToString(),
                    Data = u.CreaateOn.ToString("dd/MM/yyyy HH:mm"),
                    TotalPrice =u.TotalPrice.ToString("f2"),
                    IsSent = u.IsSent,

                    CountryName = u.Address.CountryName,
                    CityName = u.Address.CityName,
                    Street = u.Address.Street,
                    PhoneNumber = u.Address.PhoneNumber,
                    DeliveryCompanyName = u.Address.DeliveryCompany.Name,
                    MethodPayment = u.Address.MethodPayment.Method,

                }).ToArrayAsync();
            return orders;
        }

        public async Task<bool> ExistsByIdAsync(string orderId)
        {
            bool exists = await repository
                .All<UserOrder>()
                .Include(u => u.Order)
               .AnyAsync(b => b.Order.Id.ToString() == orderId);

            return exists;
        }

        public async Task EditSentToTrueAsync(string orderId)
        {
            UserOrder userOrder = await this.repository
                .All<UserOrder>()
                .Include(u => u.Order)
                .FirstAsync(u=>u.Order.Id.ToString()==orderId);

            userOrder.IsSent = true;

            await this.repository.SaveChangesAsync();
        }

    }
}
