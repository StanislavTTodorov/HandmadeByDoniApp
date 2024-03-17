using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> ExistsByIdAsync(string glassId)
        {
           bool exists = await this.dbContext.Glasses
                .AnyAsync(g=>g.Id.ToString()==glassId);
            
            return exists;
        }

        public async Task<GlassDetailsViewModel> GetGlassDetailsByIdAsync(string glassId)
        {
            Glass glass = await this.dbContext
                .Glasses
                .Include(g => g.GlassCategory)
                .FirstAsync(g => g.Id.ToString() == glassId);

            return new GlassDetailsViewModel
            { 
                Id = glass.Id.ToString(),
                Title = glass.Title,
                Description= glass.Description,
                ImageUrl = glass.ImageUrl,
                Capacity = glass.Capacity,
                Price = glass.Price,
                Category = glass.GlassCategory.Name,
                IsSet = glass.IsSet,
            };

        }
    }
}
