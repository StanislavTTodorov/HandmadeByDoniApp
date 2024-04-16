
using HandmadeByDoniApp.Services.Tests.DatabaseSeed;

namespace HandmadeByDoniApp.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Interfaces;
    using HandmadeByDoniApp.Data;
    using static DatabaseSeederBox;

#pragma warning disable CS0105 // Using directive appeared previously in this namespace
    using HandmadeByDoniApp.Data;
#pragma warning restore CS0105 // Using directive appeared previously in this namespace
    using HandmadeByDoniApp.Services.Data.Service;
    using HandmadeByDoniApp.Services.Data.DataRepository;

    public class BoxServiceTests
    {
        private DbContextOptions<HandmadeByDoniAppDbContext> dbOptions;
        private IRepository repository;

        private IBoxService boxService;

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

            this.boxService = new BoxService(this.repository);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnTrueWhenExists()
        {
            string existingId = FirstBox.Id.ToString();

            bool result = await this.boxService.ExistsByIdAsync(existingId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnFalseWhenExists()
        {
            string existingId = Guid.NewGuid().ToString();

            bool result = await this.boxService.ExistsByIdAsync(existingId);

            Assert.IsFalse(result);
        }

    }
}