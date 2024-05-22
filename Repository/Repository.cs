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
        T IRepository<T>.Get(Guid id)
        {
            try
            {
                var res = entities.AsNoTracking().AsEnumerable().FirstOrDefault(i => i.CustomerId == id);
                return res;
            }
            catch(Exception)
            {
                throw;
            }
        }
        IEnumerable<T> IRepository<T>.GetAll()
        {
            try
            {
                return entities.AsEnumerable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        Guid IRepository<T>.Insert(T entity)
        {
            try
            {
                entities.Add(entity);
                return entity.CustomerId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        void IRepository<T>.SaveChanges()
        {
            _dataContext.SaveChanges();
        }
        void IRepository<T>.Update(T entity)
        {
            _dataContext.Update(entity);
        }
    }
}
