

using HandmadeByDoniApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.DataRepository
{
    public class Repository : IRepository
    {
        private readonly DbContext dbContext;

        public Repository(HandmadeByDoniAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<T> DbSet<T>() where T : class
        {
            return this.dbContext.Set<T>();
        }

        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>();
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return DbSet<T>()
                .AsNoTracking();

        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddRangeAsync(entity);
        }

        public Task DeleteAsync<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
           return await this.dbContext.SaveChangesAsync();
        }
        public async Task<int> Save<T>(T entity)where T : class
        {
            this.dbContext.Update(entity);
 
          return await this.dbContext.SaveChangesAsync();
        }

    }
}
