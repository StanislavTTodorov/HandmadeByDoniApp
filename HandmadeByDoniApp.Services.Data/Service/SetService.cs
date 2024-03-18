using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class SetService : ISetService
    {
        private readonly IRepository repository;

        public SetService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> ExistsByIdAsync(string setId)
        {
            bool result = await this.repository
                .All<Set>()
                .AnyAsync(s => s.Id.ToString() == setId);

            return result;
        }
    }
}
