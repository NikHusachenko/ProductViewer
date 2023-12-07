namespace ProductViewer.DataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        Task Create(T entity);
        Task Delete(T entity);
        Task Update(T entity);

        Task<int> Count(string query, object obj);
        Task<int> Max(string query);

        Task<T> Find(long id);
        Task<T> First(string query, object obj);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(string query, object obj);
        Task<IEnumerable<T>> GetChunk(int skip, int take);
    }
}