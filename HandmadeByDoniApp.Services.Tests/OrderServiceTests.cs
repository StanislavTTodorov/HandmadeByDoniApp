using HandmadeByDoniApp.Services.Tests.DatabaseSeed;

namespace HandmadeByDoniApp.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Interfaces;
    using HandmadeByDoniApp.Data;
    using static DatabaseSeed.DatabaseSeederOrder;

#pragma warning disable CS0105 // Using directive appeared previously in this namespace
    using HandmadeByDoniApp.Data;
#pragma warning restore CS0105 // Using directive appeared previously in this namespace
    using HandmadeByDoniApp.Services.Data.Service;
    using HandmadeByDoniApp.Services.Data.DataRepository;
    using HandmadeByDoniApp.Services.Data.Interfaces;
    using HandmadeByDoniApp.Data.Models;

    public class OrderServiceTests
    {
        private DbContextOptions<HandmadeByDoniAppDbContext> dbOptions;
        private IRepository repository;

        private IOrderService orderService;
        private class FakeEmailService : IEmailService
        {
            public string GetConfirmEmail(string token, ApplicationUser user) => string.Empty;
            public string GetConfirmOrderEmail(UserOrder userOrder) => string.Empty;
            public Task<bool> SendEmailAsync(string toEmail, string subject, string body) => Task.FromResult(true);
            public string GetCancellationOrderEmail(UserOrder userOrder) => string.Empty;
            public string GetOrderSentEmail(UserOrder userOrder) => string.Empty;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<HandmadeByDoniAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var contex = new HandmadeByDoniAppDbContext(this.dbOptions);
            this.repository = new Repository(contex);

            contex.Database.EnsureCreated();
            SeedDatabaseOrder(contex);

            this.orderService = new OrderService(this.repository, new FakeEmailService());
        }

        [Test]
        public async Task UserOrderExistsByOrderIdAsyncShouldReturnTrueWhenExists()
        {
            string orderId = FirstUserOrder.OrderId.ToString();

            bool result = await this.orderService.UserOrderExistsByOrderIdAsync(orderId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task UserOrderExistsByOrderIdAsyncShouldReturnFalseWhenExists()
        {
            string orderId = Guid.NewGuid().ToString();

            bool result = await this.orderService.UserOrderExistsByOrderIdAsync(orderId);

            Assert.IsFalse(result);
        }
        [Test]
        public async Task UserOrderIsSentByOrderIdAsyncShouldReturnFalseWhenExists()
        {
            string orderId = FirstUserOrder.OrderId.ToString();

            bool result = await this.orderService.UserOrderIsSentByOrderIdAsync(orderId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task UserOrderIsSentByOrderIdAsyncShouldReturnTrueWhenExists()
        {
            string orderId = SecondUserOrder.OrderId.ToString();

            bool result = await this.orderService.UserOrderIsSentByOrderIdAsync(orderId);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task UserOrderExistsByUserIdAsyncShouldReturnFalseWhenExists()
        {
            string userId = SecondUser.Id.ToString();

            bool result = await this.orderService.UserOrderExistsByUserIdAsync(userId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task UserOrderExistsByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string userId = FirstUser.Id.ToString();

            bool result = await this.orderService.UserOrderExistsByUserIdAsync(userId);

            Assert.IsTrue(result);
        }
    }
}
