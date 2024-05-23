using Domain.Models;

namespace Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Guid id);
        Task<Guid> Insert(T entity);
        Task Update(T entity);
        Task SaveChanges();
    }
}
