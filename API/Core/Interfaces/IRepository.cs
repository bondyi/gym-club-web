namespace API.Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task Post(T entity);
        Task Put(T entity, int id);
        Task Delete(int id);
    }
}
