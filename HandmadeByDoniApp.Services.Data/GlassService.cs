using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Glass;

namespace HandmadeByDoniApp.Services.Data
{
    public class GlassService : IGlassService
    {
        private readonly HandmadeByDoniAppDbContext dbContext;

        public GlassService(HandmadeByDoniAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateGlassAsync(GlassFormModel formModel)
        {
           Glass newGlass = new Glass()
           { 
               Title = formModel.Title,
               Description = formModel.Description,
               ImageUrl = formModel.ImageUrl,
               Capacity = formModel.Capacity,
               Price = formModel.Price,
               GlassCategoryId = formModel.CategoryId,
               IsSet = formModel.IsSet,
           };

            await dbContext.Glasses.AddRangeAsync(newGlass);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
