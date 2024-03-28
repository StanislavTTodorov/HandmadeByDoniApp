

namespace HandmadeByDoniApp.Services.Data.DataRepository
{
    public interface IRepository
    {
        IQueryable<T> All<T>() where T : class;

        IQueryable<T> AllReadOnly<T>() where T : class;

        Task AddAsync<T>(T entity) where T : class;

        Task AddRangeAsync<T>(T entity) where T : class;

        Task DeleteAsync<T>(T entity) where T : class;

        Task<int> SaveChangesAsync();

        Task<int> Save<T>(T entity) where T : class;

    }
}
