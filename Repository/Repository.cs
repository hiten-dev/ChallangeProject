using ChallengeApplication.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dataContext;
        private DbSet<T> entities;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            entities = _dataContext.Set<T>();
        }
        async Task<T> IRepository<T>.Get(Guid id)
        {
            try
            {
                return await entities.AsNoTracking().FirstOrDefaultAsync(i => i.CustomerId == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        async Task<IEnumerable<T>> IRepository<T>.GetAll()
        {
            try
            {
                return await entities.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        async Task<Guid> IRepository<T>.Insert(T entity)
        {
            try
            {
                await entities.AddAsync(entity);
                return entity.CustomerId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        async Task IRepository<T>.SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }
        async Task IRepository<T>.Update(T entity)
        {
            entities.Update(entity);
            await Task.CompletedTask;
        }
    }
}
