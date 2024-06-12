
using HandmadeByDoniApp.Services.Tests.DatabaseSeed;

namespace HandmadeByDoniApp.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Interfaces;
    using HandmadeByDoniApp.Data;
    using static DatabaseSeederProduct;

#pragma warning disable CS0105 // Using directive appeared previously in this namespace
    using HandmadeByDoniApp.Data;
#pragma warning restore CS0105 // Using directive appeared previously in this namespace
    using HandmadeByDoniApp.Services.Data.Service;
    using HandmadeByDoniApp.Services.Data.DataRepository;

    public class ProductServiceTests
    {
        private DbContextOptions<HandmadeByDoniAppDbContext> dbOptions;
        private IRepository repository;

        private IProductService productService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<HandmadeByDoniAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var contex = new HandmadeByDoniAppDbContext(this.dbOptions);
            this.repository = new Repository(contex);

            contex.Database.EnsureCreated();
            SeedDatabaseBox(contex);

            this.productService = new ProductService(this.repository);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnTrueWhenExists()
        {
            string existingId = FirstBox.Id.ToString();

            bool result = await this.productService.ExistsByIdAsync(existingId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnFalseWhenExists()
        {
            string existingId = Guid.NewGuid().ToString();

            bool result = await this.productService.ExistsByIdAsync(existingId);

            Assert.IsFalse(result);
        }

    }
}