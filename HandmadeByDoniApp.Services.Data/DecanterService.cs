

using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Decanter;

namespace HandmadeByDoniApp.Services.Data
{
    public class DecanterService : IDecanterService
    {
        private readonly HandmadeByDoniAppDbContext dbContext;

        public DecanterService(HandmadeByDoniAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateDecanterAsync(DecanterFormModel formModel)
        {
            Decanter newDecanter = new Decanter()
            {
                Title = formModel.Title,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                Capacity = formModel.Capacity,
                Price = formModel.Price,
            };

            await dbContext.Decaners.AddRangeAsync(newDecanter);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
