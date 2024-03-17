

using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data
{
    public class SetService : ISetService
    {
        private readonly HandmadeByDoniAppDbContext dbContext;

        public SetService(HandmadeByDoniAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(string setId)
        {
            bool result = await this.dbContext.Sets
                .AnyAsync(s=>s.Id.ToString()==setId);

            return result;
        }
    }
}
