

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

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            DbSet<T>().Remove(entity);

            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        public async Task<int> Update<T>(T entity) where T : class
        {
            this.DbSet<T>().Update(entity);

            return await SaveChangesAsync();
        }

    }
}
