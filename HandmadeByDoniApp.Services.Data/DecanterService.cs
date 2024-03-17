

using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> ExistsByIdAsync(string decanterId)
        {
            bool result = await this.dbContext.Decaners
                .AnyAsync(d=>d.Id.ToString()==decanterId);

            return result;
        }

        public async Task<DecanterDetailsViewModel> GetDecanterDetailsByIdAsync(string decanterId)
        {
            Decanter decanter = await this.dbContext
               .Decaners
               .FirstAsync(g => g.Id.ToString() == decanterId);

            return new DecanterDetailsViewModel
            {
                Id = decanter.Id.ToString(),
                Title = decanter.Title,
                Description = decanter.Description,
                ImageUrl = decanter.ImageUrl,
                Capacity = decanter.Capacity,
                Price = decanter.Price,
                IsSet = decanter.IsSet,
            };
        }
    }
}
