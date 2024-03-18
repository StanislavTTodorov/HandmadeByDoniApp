using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class DecanterService : IDecanterService
    {
        private readonly IRepository repository;

        public DecanterService(IRepository repository)
        {
            this.repository = repository;
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

            await repository.AddAsync(newDecanter);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string decanterId)
        {
            bool result = await repository.All<Decanter>()
                .AnyAsync(d => d.Id.ToString() == decanterId);

            return result;
        }

        public async Task<DecanterDetailsViewModel> GetDecanterDetailsByIdAsync(string decanterId)
        {
            Decanter decanter = await repository
               .All<Decanter>()
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
