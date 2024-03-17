
using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Box;
using Microsoft.EntityFrameworkCore;


namespace HandmadeByDoniApp.Services.Data
{
    public class BoxService : IBoxService
    {
        private readonly HandmadeByDoniAppDbContext dbContext;

        public BoxService(HandmadeByDoniAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateBoxAsync(BoxFormModel formModel)
        {
            Box newBox = new Box()
            {
                Title = formModel.Title,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                Capacity = formModel.Capacity,
                Price = formModel.Price,              
            };

            await dbContext.Boxs.AddRangeAsync(newBox);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string boxId)
        {
            bool exists = await this.dbContext.Boxs
                .AnyAsync(b=>b.Id.ToString()==boxId);

            return exists;
        }

        public async Task<BoxDetailsViewModel> GetBoxDetailsByIdAsync(string boxId)
        {
            Box box = await this.dbContext
           .Boxs
           .FirstAsync(g => g.Id.ToString() == boxId);

            return new BoxDetailsViewModel
            {
                Id = box.Id.ToString(),
                Title = box.Title,
                Description = box.Description,
                ImageUrl = box.ImageUrl,
                Capacity = box.Capacity,
                Price = box.Price,
            };

        }
    }
}
