using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Box;
using Microsoft.EntityFrameworkCore;


namespace HandmadeByDoniApp.Services.Data.Service
{
    public class BoxService : IBoxService
    {
        private readonly IRepository repository;

        public BoxService(IRepository repository)
        {
            this.repository = repository;
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

            await repository.AddAsync(newBox);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string boxId)
        {
            bool exists = await repository.All<Box>()
                .AnyAsync(b => b.Id.ToString() == boxId);

            return exists;
        }

        public async Task<BoxDetailsViewModel> GetBoxDetailsByIdAsync(string boxId)
        {
            Box box = await repository
                .All<Box>()
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
