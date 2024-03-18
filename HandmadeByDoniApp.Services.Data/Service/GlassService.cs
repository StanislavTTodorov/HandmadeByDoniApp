using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class GlassService : IGlassService
    {
        private readonly IRepository repository;

        public GlassService(IRepository repository)
        {
            this.repository = repository;
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

            await repository.AddRangeAsync(newGlass);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string glassId)
        {
            bool exists = await this.repository.AllReadOnly<Glass>()
                 .AnyAsync(g => g.Id.ToString() == glassId);

            return exists;
        }

        public async Task<GlassDetailsViewModel> GetGlassDetailsByIdAsync(string glassId)
        {
            Glass glass = await this.repository
                .AllReadOnly<Glass>()
                .Include(g => g.GlassCategory)
                .FirstAsync(g => g.Id.ToString() == glassId);

            return new GlassDetailsViewModel
            {
                Id = glass.Id.ToString(),
                Title = glass.Title,
                Description = glass.Description,
                ImageUrl = glass.ImageUrl,
                Capacity = glass.Capacity,
                Price = glass.Price,
                Category = glass.GlassCategory.Name,
                IsSet = glass.IsSet,
            };

        }
    }
}
