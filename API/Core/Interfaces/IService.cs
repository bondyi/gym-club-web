namespace API.Core.Interfaces
{
    public interface IService<Dto>
    {
        Task<Dto?> Get(int id);
        Task<IEnumerable<Dto>> GetAll();
        Task Post(Dto entity);
        Task Put(int id, Dto entity);
        Task Delete(int id);
    }
}
