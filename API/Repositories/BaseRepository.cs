using API.Core.Interfaces;
using API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public abstract class BaseRepository<T>(GymClubContext context) : IRepository<T>
    {
        public async Task Delete(int id)
        {
            var entity = await context.FindAsync(typeof(T), id) ?? throw new NullReferenceException();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T?> Get(int id)
        {
            var entity = await context.FindAsync(typeof(T), id);
            return entity == null ? throw new NullReferenceException() : (T)entity;
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotSupportedException();
        }

        public async Task Post(T entity)
        {
            if (entity == null) throw new NullReferenceException();
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task Put(int id, T entity)
        {
            if (entity == null) throw new NullReferenceException();
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
