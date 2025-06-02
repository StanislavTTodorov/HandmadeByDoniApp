
using HandmadeByDoniApp.Common;
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
        private IEmailService emailService;

        public OrderService(IRepository repository, IEmailService emailService)
        {
            this.repository = repository;
            this.emailService = emailService;
        }

        public async Task<MineProductViewModel> AllMineProductsByUserIdAsync(string userId)
        {
            ApplicationUser? user = await GetProductsForUserByUserIdAsync(userId);

            List<ProductsAllViewModel> products = new List<ProductsAllViewModel>();
            if (user != null)
            {
                products = user.Products.Select(x => new ProductsAllViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    Price = x.Price,
                    IsActive = x.IsActive,
                    ImageUrl = x.ImageUrl
                }).ToList();
            }
            MineProductViewModel viewModel = new MineProductViewModel()
            {
                totalPrice = products.Sum(x => x.Price),
                Products = products
            };

            return viewModel;
        }

        public async Task<bool> CreateRegisterUserOrderByUserIdAsync(string userId)
        {
            ApplicationUser user = await GetProductsForUserByUserIdAsync(userId);

            Order newOrder = new Order()
            {
                User = user,
                ClientId = user.Id,
                Products = new HashSet<Product>()
            };

            Product[] newProduct = new Product[user.Products.Count];

            user.Products.CopyTo(newProduct, 0);
            foreach (var glass in user.Products)
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
            user.Products.Clear();          
            newOrder.Products = newProduct;

            await repository.AddAsync(newOrder);

            Address address = await this.repository
                              .All<Address>()
                              .Include(a => a.DeliveryCompany)
                              .Include(a => a.MethodPayment)
                              .FirstAsync(n => n.ClientId == user.Id);

            UserOrder userOrder = new UserOrder()
            {
                User = user,
                UserId = user.Id,
                OrderId = newOrder.Id,
                Order = newOrder,
                TotalPrice = newOrder.Products.Sum(x => x.Price),
                CreaateOn = DateTime.Now,
                Address = address,
                AddressId = address.Id,
                IsSent = false,
            };
            await repository.AddAsync(userOrder);
            await repository.SaveChangesAsync();

            string body = emailService.GetConfirmOrderEmail(userOrder); 
            await emailService.SendEmailAsync(userOrder.User.Email, $"Поръчка от {GeneralMessages.Name}", body);

            return true;
        }

        public async Task AddProductByUserIdAsync(string userId, string productId)
        {
            bool add = true;
            await AddOrRemoveProductByUserIdAsync(userId, productId, add);          
        }
       
        public async Task RemoveProductByUserIdAsync(string userId, string productId)
        {
            bool add = false;
            await AddOrRemoveProductByUserIdAsync(userId, productId, add);
        }
        
        public async Task<bool> IsActiveByIdAsync(string id)
        {
            bool product = await this.repository
                .AllReadOnly<Product>()
                .Where(g => g.IsActive == true)
                .AnyAsync(g => g.Id.ToString() == id);

            return product;
        }

        public async Task<bool> UserOrderExistsByUserIdAsync(string userId)
        {
            bool exists = await this.repository
                .AllReadOnly<UserOrder>()
                .AnyAsync(u => u.UserId.ToString() == userId);

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
                        IsSent = u.IsSent,
                        ShipmentNoteNumber = u.ShipmentNoteNumber?? "",
                        
                    }
                })
                .ToArrayAsync();
            return orders;
        }

        public async Task<MineProductViewModel> AllOrderProductsByOrderIdAsync(string orderId)
        {
            Order? order = await this.repository
                 .All<Order>()
                 .Include(u => u.Products)
                 .Where(u => u.Id.ToString() == orderId)
                 .FirstOrDefaultAsync();
            List<ProductsAllViewModel> products = new List<ProductsAllViewModel>();
            if (order != null)
            {
                products = order.Products.Select(x => new ProductsAllViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    Price = x.Price,
                    IsActive = x.IsActive,
                    ImageUrl = x.ImageUrl
                }).ToList();
            }
            MineProductViewModel viewModel = new MineProductViewModel()
            {
                totalPrice = products.Sum(x => x.Price),
                Products = products
            };
            return viewModel;
        }

        public async Task<ICollection<AdminOrdersViewModel>> GetUserOrdersAsync()
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
                    TotalPrice = u.TotalPrice.ToString("f2"),
                    IsSent = u.IsSent,
                    ShipmentNoteNumber = u.ShipmentNoteNumber,

                    CountryName = u.Address.CountryName,
                    CityName = u.Address.CityName,
                    Street = u.Address.Street,
                    PhoneNumber = u.Address.PhoneNumber,
                    DeliveryCompanyName = u.Address.DeliveryCompany.Name,
                    MethodPayment = u.Address.MethodPayment.Method,

                }).ToArrayAsync();
            return orders;
        }

        public async Task<bool> UserOrderExistsByOrderIdAsync(string orderId)
        {
            bool exists = await this.repository
                .All<UserOrder>()
                .Include(u => u.Order)
               .AnyAsync(b => b.Order.Id.ToString() == orderId);

            return exists;
        }

        public async Task EditSentToTrueAsync(string orderId, string? ShipmentNoteNumber = null)
        {
            UserOrder userOrder = await GetUserOrdersByOrderIdAsync(orderId);

            userOrder.IsSent = true;
            if (ShipmentNoteNumber != null)
            {
                userOrder.ShipmentNoteNumber = ShipmentNoteNumber;
            }

            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteUserOrderByOrderIdAsync(string orderId)
        {
            UserOrder userOrder = await GetUserOrdersByOrderIdAsync(orderId);

            Order order = await this.repository
                .All<Order>()
                .Include(o => o.Products)
                .FirstAsync(u => u.Id.ToString() == orderId);

            IsActiveTurnOnTrue(order.Products);

            await this.repository.DeleteAsync(userOrder);
            await this.repository.DeleteAsync(order);
        }

        public async Task<bool> UserOrderIsSentByOrderIdAsync(string orderId)
        {
            UserOrder userOrder = await GetUserOrdersByOrderIdAsync(orderId);
            return userOrder.IsSent;
        }

        public async Task<EditOrderViewModel> GetUserOrderByOrdeIdAsync(string orderId)
        {
            UserOrder userOrder= await this.repository.All<UserOrder>()
                                                        .Include(u => u.User)
                                                        .Include(u => u.Order)
                                                        .Include(a=>a.Address)                                                   
                                                        .ThenInclude(m=>m.MethodPayment)
                                                        .FirstAsync(u => u.OrderId.ToString() == orderId);

            if(userOrder == null)
            {
                return new EditOrderViewModel();
            }

            return new EditOrderViewModel()
            {
                ShipmentNoteNumber = userOrder.ShipmentNoteNumber?? "",
                Id = userOrder.OrderId.ToString(),
                UserEmail = userOrder.User.Email,
                UserName = $"{userOrder.User.FirstName} {userOrder.User.LastName}",
                Address = $"{userOrder.Address.CountryName}, {userOrder.Address.CityName}, {userOrder.Address.Street}",
                UserPhone = userOrder.Address.PhoneNumber,
                MethodPayment = userOrder.Address.MethodPayment.Method
                //IsSent = userOrder.IsSent,
            };
        }

        private async Task<Product> GetProductByIdAsync(string productId)
        {
            return await this.repository
                    .All<Product>()
                    .Where(b => b.Id.ToString() == productId)
                    .FirstAsync();
        }

        private async Task<ApplicationUser> GetProductsForUserByUserIdAsync(string userId)
        {
            return await this.repository
                .All<ApplicationUser>()
                .Include(u => u.Products)
                .FirstAsync(u => u.Id.ToString() == userId);
        }

        private async Task<UserOrder> GetUserOrdersByOrderIdAsync(string orderId)
        {
            return await this.repository
                  .All<UserOrder>()
                  .Include(u => u.Order)
                  .FirstAsync(u => u.Order.Id.ToString() == orderId);
        }

        private async Task AddOrRemoveProductByUserIdAsync(string userId, string productId, bool add)
        {
            ApplicationUser? user = await this.GetProductsForUserByUserIdAsync(userId);

            if (user != null)
            {
                Product product = await GetProductByIdAsync(productId);
                if (add)
                {
                    user.Products.Add(product);
                }
                else
                {
                    user.Products.Remove(product);
                }

                await this.repository.SaveChangesAsync();
            }
        }

        private void IsActiveTurnOnTrue(ICollection<Product> products)
        {
            foreach (var product in products)
            {
                product.IsActive = true;
                product.OrderId = null;
            }
        }
    }
}
